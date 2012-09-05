﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.EventStore;
using Ziggurat.Infrastructure.Projections;

namespace Ziggurat.Client.Setup.ProjectionRebuilder
{
    public sealed class Rebuilder
    {
        private readonly IEventStore _eventStore;
        private readonly IProjectionStoreFactory _realStoreFactory;
        private readonly IProjectionStoreFactory _inMemoryProjectionStore;
        private readonly Func<IProjectionStoreFactory, IEnumerable<object>> _howToBuildProjections;

        private readonly IProjectionWriter<string, ProjectionsSignatures> _signatureWriter;
        private readonly IProjectionReader<string, ProjectionsSignatures> _signatureReader;

        public Rebuilder(
            IEventStore eventStore,
            IProjectionStoreFactory realStoreFactory, 
            Func<IProjectionStoreFactory, IEnumerable<object>> howToBuildProjections)
        {
            if (eventStore == null) throw new ArgumentNullException("eventStore");
            if (realStoreFactory == null) throw new ArgumentNullException("realStoreFactory");
            if (howToBuildProjections == null) throw new ArgumentNullException("howToBuildProjections");

            _eventStore       = eventStore;
            _realStoreFactory = realStoreFactory;

            _howToBuildProjections   = howToBuildProjections;

            _signatureReader = realStoreFactory.GetReader<string, ProjectionsSignatures>();
            _signatureWriter = realStoreFactory.GetWriter<string, ProjectionsSignatures>();
        }

        public void Run()
        {
            var inMemoryStore = new InMemoryProjectionStoreFactory();
            var intermediateStore = new ProjectionFactoryWrapper(inMemoryStore);

            var registeredProjections    = BuildProjections(intermediateStore);
            var knownProjections         = GetKnownProjectionsSignatures();
            var realProjectionSignatures = GetProjectionsSignatures(registeredProjections);

            var projectionsToRebuild  = GetProjectionsToRebuild(realProjectionSignatures, knownProjections.TypeSignatures);

            if (projectionsToRebuild.Count == 0) return;

            Console.WriteLine("Projections to rebuild: {0}, running...", projectionsToRebuild.Count);
            foreach (var prj in projectionsToRebuild)
            {
                Console.WriteLine("\t" + prj.Key.GetType().Name);
            }

            DoRebuildProjections(projectionsToRebuild.Keys.ToList());

            WriteProjectionsToRealStore(inMemoryStore, intermediateStore);

            PersistProjectionSignatures(realProjectionSignatures);
        }

        private void WriteProjectionsToRealStore(InMemoryProjectionStoreFactory inMemoryStore, ProjectionFactoryWrapper intermediateStore)
        {
            Console.WriteLine("Saving rebuilt projections...");

            var timer = new Stopwatch();
            var allSets = inMemoryStore.GetAllSets();
            foreach (var set in allSets)
            {
                var streamer = intermediateStore.CreateStreamerFor(set.Key, _realStoreFactory);

                foreach (DictionaryEntry data in set.Value)
                {
                    streamer(data.Key, data.Value);
                }
            }
            timer.Stop();
            Console.WriteLine("Saved, it took {0}", timer.Elapsed);
        }

        private void DoRebuildProjections(List<object> projections)
        {
            var dispatcher = new ConventionalToWhenDispatcher();
            projections.ForEach(x => dispatcher.Subscribe(x));

            var timer = new Stopwatch();
            var allEvents = _eventStore.LoadSince(0);

            foreach (var evt in allEvents)
                dispatcher.DispatchToAll(evt.Body);

            timer.Stop();
            Console.WriteLine("Rebuilding projections took: {0}", timer.Elapsed);
        }

        private void PersistProjectionSignatures(IDictionary<object, string> realProjectionSignatures)
        {
            var typesAndSignatures = realProjectionSignatures
                .ToDictionary(x => x.Key.GetType().FullName, x => x.Value);

            _signatureWriter.AddOrReplace("rebuilder", new ProjectionsSignatures(typesAndSignatures));
        }

        private Dictionary<object, string> GetProjectionsToRebuild(
            IDictionary<object, string> existingProjections,
            IDictionary<string, string> knownProjections)
        {
            return existingProjections
                .Where(x => x.Value != GetDictionaryValueOrDefault(knownProjections, x.Key.GetType().FullName))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        private static TValue GetDictionaryValueOrDefault<TKey, TValue>(IDictionary<TKey, TValue> dict, TKey key)
        {
            TValue value;
            dict.TryGetValue(key, out value);

            return value;
        }

        private IList<object> BuildProjections(IProjectionStoreFactory storeFactory)
        {
            return _howToBuildProjections(storeFactory).ToList();
        }

        private IDictionary<object, string> GetProjectionsSignatures(IList<object> projections)
        {
            return projections
                .ToDictionary(x => x, x => TypeSignatureBuilder.GetSignatureForType(x.GetType()));
        }

        private ProjectionsSignatures GetKnownProjectionsSignatures()
        {
            return _signatureReader.LoadOrDefault("rebuilder", x => new ProjectionsSignatures());
        }
    }
}

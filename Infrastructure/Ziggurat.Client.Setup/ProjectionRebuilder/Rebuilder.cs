using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Infrastructure.Projections;

namespace Ziggurat.Client.Setup.ProjectionRebuilder
{
    public sealed class Rebuilder
    {
        private readonly IProjectionStoreFactory _realStoreFactory;
        private readonly IProjectionStoreFactory _inMemoryProjectionStore;
        private readonly Func<IProjectionStoreFactory, IEnumerable<object>> _howToBuildProjections;

        private readonly IProjectionWriter<string, ProjectionsSignatures> _signatureWriter;
        private readonly IProjectionReader<string, ProjectionsSignatures> _signatureReader;

        public Rebuilder(
            IProjectionStoreFactory realStoreFactory, 
            Func<IProjectionStoreFactory, IEnumerable<object>> howToBuildProjections)
        {
            if (realStoreFactory == null) throw new ArgumentNullException("realStoreFactory");
            if (howToBuildProjections == null) throw new ArgumentNullException("howToBuildProjections");

            _realStoreFactory = realStoreFactory;

            _inMemoryProjectionStore = new InMemoryProjectionStoreFactory();
            _howToBuildProjections   = howToBuildProjections;

            _signatureReader = realStoreFactory.GetReader<string, ProjectionsSignatures>();
            _signatureWriter = realStoreFactory.GetWriter<string, ProjectionsSignatures>();
        }

        public void Run()
        {
            var registeredProjections = BuildProjections();
            var knownProjections      = GetKnownProjectionsSignatures();
            var realProjections       = GetProjectionsSignatures(registeredProjections);

            var projectionsToRebuild  = GetProjectionsToRebuild(realProjections, knownProjections.TypeSignature);

            if (projectionsToRebuild.Count > 0)
            {
                Console.WriteLine("Projections to rebuild: ");
                foreach (var prj in projectionsToRebuild)
                {
                    Console.WriteLine("\t" + prj.Key.GetType().Name);
                }
            }
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

        private IList<object> BuildProjections()
        {
            return _howToBuildProjections(_inMemoryProjectionStore).ToList();
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

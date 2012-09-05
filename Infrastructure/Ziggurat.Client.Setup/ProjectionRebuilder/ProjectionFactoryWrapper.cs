using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Infrastructure.DocumentStore;

namespace Ziggurat.Client.Setup.ProjectionRebuilder
{
    public sealed class ProjectionFactoryWrapper : IDocumentStore
    {
        private ConcurrentDictionary<Tuple<Type, Type>, IntermediateWriterInfo> _cachedWriterInfos
            = new ConcurrentDictionary<Tuple<Type, Type>, IntermediateWriterInfo>();

        private readonly IDocumentStore _innerFactory;
        public ProjectionFactoryWrapper(IDocumentStore innerFactory)
        {
            _innerFactory = innerFactory;
        }

        public IDocumentReader<TKey, TView> GetReader<TKey, TView>()
        {
            var innerReader = _innerFactory.GetReader<TKey, TView>();
            return innerReader;
        }

        public IDocumentWriter<TKey, TView> GetWriter<TKey, TView>()
        {
            var cachedWriter = _cachedWriterInfos.GetOrAdd(
                Tuple.Create(typeof(TKey), typeof(TView)),
                key =>
                {
                    var intermediateWriter = _innerFactory.GetWriter<TKey, TView>();
                    return new IntermediateWriterInfo(
                        intermediateWriter,
                        storeFactory => GenerateUntypedCallingAction<TKey, TView>(storeFactory));
                });

            return (IDocumentWriter<TKey, TView>)cachedWriter.IntermediateWriter;
        }

        private Action<object, object> GenerateUntypedCallingAction<TKey, TView>(IDocumentStore storeFactory)
        {
            var typedWriter = storeFactory.GetWriter<TKey, TView>();
            return (key, view) =>
            {
                var typedKey = (TKey)key;
                var typedView = (TView)view;
                typedWriter.AddOrUpdate(typedKey,
                    () => typedView,
                    oldView => typedView);
            };
        }

        public Action<object, object> CreateStreamerFor(Tuple<Type, Type> keyAndViewTypes, IDocumentStore streamTo)
        {
            var info = _cachedWriterInfos[keyAndViewTypes];
            return info.CreateStreamer(streamTo);
        }

        public sealed class IntermediateWriterInfo
        {
            public Func<IDocumentStore, Action<object, object>> CreateStreamer { get; private set; }
            public object IntermediateWriter { get; private set; }

            public IntermediateWriterInfo(
                object intermediateWriter, 
                Func<IDocumentStore, Action<object, object>> createStreamer)
            {
                IntermediateWriter = intermediateWriter;
                CreateStreamer = createStreamer;
            }

        }
    }
}

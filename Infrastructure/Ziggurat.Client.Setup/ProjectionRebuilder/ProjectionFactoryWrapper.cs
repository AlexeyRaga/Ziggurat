using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Infrastructure.Projections;

namespace Ziggurat.Client.Setup.ProjectionRebuilder
{
    public sealed class ProjectionFactoryWrapper : IProjectionStoreFactory
    {
        private ConcurrentDictionary<Tuple<Type, Type>, IntermediateWriterInfo> _cachedWriterInfos
            = new ConcurrentDictionary<Tuple<Type, Type>, IntermediateWriterInfo>();

        private readonly IProjectionStoreFactory _innerFactory;
        public ProjectionFactoryWrapper(IProjectionStoreFactory innerFactory)
        {
            _innerFactory = innerFactory;
        }

        public IProjectionReader<TKey, TView> GetReader<TKey, TView>()
        {
            var innerReader = _innerFactory.GetReader<TKey, TView>();
            return innerReader;
        }

        public IProjectionWriter<TKey, TView> GetWriter<TKey, TView>()
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

            return (IProjectionWriter<TKey, TView>)cachedWriter.IntermediateWriter;
        }

        private Action<object, object> GenerateUntypedCallingAction<TKey, TView>(IProjectionStoreFactory storeFactory)
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

        public sealed class IntermediateWriterInfo
        {
            public Func<IProjectionStoreFactory, Action<object, object>> CreateStreamer { get; private set; }
            public object IntermediateWriter { get; private set; }

            public IntermediateWriterInfo(
                object intermediateWriter, 
                Func<IProjectionStoreFactory, Action<object, object>> createStreamer)
            {
                IntermediateWriter = intermediateWriter;
                CreateStreamer = createStreamer;
            }

        }
    }
}

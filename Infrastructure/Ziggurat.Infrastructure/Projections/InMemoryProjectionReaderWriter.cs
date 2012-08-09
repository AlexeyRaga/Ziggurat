using System;
using System.Collections.Concurrent;

namespace Ziggurat.Infrastructure.Projections
{
    public sealed class InMemoryProjectionReaderWriter<TKey, TView> : IProjectionReader<TKey, TView>, IProjectionWriter<TKey, TView>
    {
        private static readonly ConcurrentDictionary<TKey, TView> Mappings = new ConcurrentDictionary<TKey, TView>();

        public bool TryGet(TKey key, out TView view)
        {
            return Mappings.TryGetValue(key, out view);
        }

        public bool Exists(TKey key)
        {
            return Mappings.ContainsKey(key);
        }

        public TView AddOrUpdate(TKey key, Func<TView> addFactory, Func<TView, TView> updateFactory)
        {
            return Mappings.AddOrUpdate(key, k => addFactory(), (k, v) => updateFactory(v));
        }

        public bool TryDelete(TKey key)
        {
            TView view;
            return Mappings.TryRemove(key, out view);
        }
    }
}

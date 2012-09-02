using System;
using System.Collections;
using System.Collections.Concurrent;

namespace Ziggurat.Infrastructure.Projections
{
    public sealed class InMemoryProjectionStoreFactory : IProjectionStoreFactory
    {
        private readonly ConcurrentDictionary<Type, IDictionary> Structure = new ConcurrentDictionary<Type, IDictionary>(); 

        public IProjectionReader<TKey, TView> GetReader<TKey, TView>()
        {
            var dictionary = GetOrCreateStorage<TKey, TView>();
            return new InMemoryProjectionReaderWriter<TKey, TView>(dictionary);
        }

        public IProjectionWriter<TKey, TView> GetWriter<TKey, TView>()
        {
            var dictionary = GetOrCreateStorage<TKey, TView>();
            return new InMemoryProjectionReaderWriter<TKey, TView>(dictionary);
        }

        private ConcurrentDictionary<TKey, TView> GetOrCreateStorage<TKey, TView>()
        {
            var dictionary = Structure.GetOrAdd(typeof(TKey), t => new ConcurrentDictionary<TKey, TView>());
            var typedDictionary = (ConcurrentDictionary<TKey, TView>)dictionary;

            return typedDictionary;
        }
    }

    public sealed class InMemoryProjectionReaderWriter<TKey, TView> : IProjectionReader<TKey, TView>, IProjectionWriter<TKey, TView>
    {
        private ConcurrentDictionary<TKey, TView> _storage;
        public InMemoryProjectionReaderWriter(ConcurrentDictionary<TKey, TView> storage)
        {
            _storage = storage;
        }

        public bool TryGet(TKey key, out TView view)
        {
            return _storage.TryGetValue(key, out view);
        }

        public bool Exists(TKey key)
        {
            return _storage.ContainsKey(key);
        }

        public TView AddOrUpdate(TKey key, Func<TView> addFactory, Func<TView, TView> updateFactory)
        {
            return _storage.AddOrUpdate(key, k => addFactory(), (k, v) => updateFactory(v));
        }

        public bool TryDelete(TKey key)
        {
            TView view;
            return _storage.TryRemove(key, out view);
        }
    }
}

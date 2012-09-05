using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Ziggurat.Infrastructure.DocumentStore
{
    public sealed class InMemoryDocumentStoreFactory : IDocumentStore
    {
        private readonly ConcurrentDictionary<Tuple<Type, Type>, IDictionary> Structure
            = new ConcurrentDictionary<Tuple<Type, Type>, IDictionary>();

        public IDictionary<Tuple<Type, Type>, IDictionary> GetAllSets()
        {
            return Structure;
        }

        public IDocumentReader<TKey, TDocument> GetReader<TKey, TDocument>()
        {
            var dictionary = GetOrCreateStorage<TKey, TDocument>();
            return new InMemoryProjectionReaderWriter<TKey, TDocument>(dictionary);
        }

        public IDocumentWriter<TKey, TDocument> GetWriter<TKey, TDocument>()
        {
            var dictionary = GetOrCreateStorage<TKey, TDocument>();
            return new InMemoryProjectionReaderWriter<TKey, TDocument>(dictionary);
        }

        private ConcurrentDictionary<TKey, TDocument> GetOrCreateStorage<TKey, TDocument>()
        {
            var dictionary = Structure.GetOrAdd(
                Tuple.Create(typeof(TKey), typeof(TDocument)),
                t => new ConcurrentDictionary<TKey, TDocument>());
            var typedDictionary = (ConcurrentDictionary<TKey, TDocument>)dictionary;

            return typedDictionary;
        }
    }

    public sealed class InMemoryProjectionReaderWriter<TKey, TDocument> : IDocumentReader<TKey, TDocument>, IDocumentWriter<TKey, TDocument>
    {
        private ConcurrentDictionary<TKey, TDocument> _storage;
        public InMemoryProjectionReaderWriter(ConcurrentDictionary<TKey, TDocument> storage)
        {
            _storage = storage;
        }

        public bool TryGet(TKey key, out TDocument document)
        {
            return _storage.TryGetValue(key, out document);
        }

        public bool Exists(TKey key)
        {
            return _storage.ContainsKey(key);
        }

        public TDocument AddOrUpdate(TKey key, Func<TDocument> addFactory, Func<TDocument, TDocument> updateFactory)
        {
            return _storage.AddOrUpdate(key, k => addFactory(), (k, v) => updateFactory(v));
        }

        public bool TryDelete(TKey key)
        {
            TDocument document;
            return _storage.TryRemove(key, out document);
        }
    }
}

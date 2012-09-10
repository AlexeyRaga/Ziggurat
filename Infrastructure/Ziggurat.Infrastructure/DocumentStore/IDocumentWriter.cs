using System;

namespace Ziggurat.Infrastructure.DocumentStore
{
    public interface IDocumentWriter<in TKey, TDocument>
    {
	    TDocument AddOrUpdate(TKey key, Func<TDocument> addFactory, Func<TDocument, TDocument> updateFactory);
	    bool TryDelete(TKey key);
        bool Exists(TKey key);
    }
}

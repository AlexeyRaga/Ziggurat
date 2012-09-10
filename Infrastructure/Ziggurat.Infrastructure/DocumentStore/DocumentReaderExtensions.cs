using System;

namespace Ziggurat.Infrastructure.DocumentStore
{
	public static class DocumentReaderExtensions
	{
		public static TDocument Load<TKey, TDocument>(this IDocumentReader<TKey, TDocument> reader, TKey key)
		{
			TDocument document;
			if (reader.TryGet(key, out document))
				return document;

			var errorMessage = String.Format("Document '{0}' with key '{1}' does not exist.", typeof(TDocument).Name, key);
			throw new InvalidOperationException(errorMessage);
		}

        public static TDocument LoadOrDefault<TKey, TDocument>(
            this IDocumentReader<TKey, TDocument> reader, 
            TKey key, 
            Func<TKey, TDocument> defaultFactory)
        {
            TDocument document;
            if (reader.TryGet(key, out document)) return document;

            return defaultFactory(key);
        }

        public static TDocument LoadOrDefault<TKey, TDocument>(
            this IDocumentReader<TKey, TDocument> reader,
            TKey key,
            TDocument defaultValue = default(TDocument))
        {
            return reader.LoadOrDefault(key, k => { return defaultValue; });
        }
	}
}

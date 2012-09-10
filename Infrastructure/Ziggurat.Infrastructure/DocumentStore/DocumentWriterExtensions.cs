using System;

namespace Ziggurat.Infrastructure.DocumentStore
{
	public static class DocumentWriterExtensions
	{
		public static TDocument AddOrReplace<TKey, TDocument>(this IDocumentWriter<TKey, TDocument> writer, TKey key, Func<TDocument> addFactory)
		{
			return writer.AddOrUpdate(key,
			                          addFactory,
			                          x => addFactory());
		}

		/// <summary>
		/// Adds the specified document or replaces an existing one
		/// </summary>
		public static TDocument AddOrReplace<TKey, TDocument>(this IDocumentWriter<TKey, TDocument> writer, TKey key, TDocument document)
		{
			return writer.AddOrReplace(key, () => document);
		}

		public static TDocument AddOrUpdate<TKey, TDocument>(this IDocumentWriter<TKey, TDocument> writer, TKey key, Func<TDocument> addFactory, Action<TDocument> updateFactory)
		{
			return writer.AddOrUpdate(key,
			                          addFactory,
			                          x =>
			                          {
			                          	updateFactory(x);
			                          	return x;
			                          });
		}

		/// <summary>
		/// Adds the specified document or updates the existing one
		/// </summary>
		public static TDocument AddOrUpdate<TKey, TDocument>(this IDocumentWriter<TKey, TDocument> writer, TKey key, TDocument document, Action<TDocument> updateFactory)
		{
			return writer.AddOrUpdate(key,
			                          () => document,
			                          x =>
			                          {
			                          	updateFactory(x);
			                          	return x;
			                          });
		}

        /// <summary>
        /// Updates the specified document. A new instance will be created if there is no document found in the store.
        /// </summary>
        public static TDocument AddOrUpdate<TKey, TDocument>(this IDocumentWriter<TKey, TDocument> writer, TKey key, Action<TDocument> updateFactory)
            where TDocument : new()
        {
            return writer.AddOrUpdate(key,
                () =>
                {
                    var document = new TDocument();
                    updateFactory(document);
                    return document;
                },
                x =>
                {
                    updateFactory(x);
                    return x;
                });
        }

		/// <summary>
		/// Adds a new document ot throws an exception if there is one already added
		/// </summary>
		public static TDocument	Add<TKey, TDocument>(this IDocumentWriter<TKey, TDocument> writer, TKey key, TDocument document)
		{
			return writer.AddOrUpdate(key,
			                          () => document,
			                          x =>
			                          {
			                          	var errorMessage = String.Format("Document '{0}' with key '{1}' is already added.",
			                          	                                 typeof (TDocument).Name,
			                          	                                 key);
			                          	throw new InvalidOperationException(errorMessage);
			                          });
		}

		/// <summary>
		/// Updates a persisted document or throw an exception if there is no document persisted for a given key
		/// </summary>
		public static TDocument Update<TKey, TDocument>(this IDocumentWriter<TKey, TDocument> writer, TKey key, Action<TDocument> updateFactory)
		{
			return writer.AddOrUpdate(key,
			                          () =>
			                          {
			                          	var errorMessage = String.Format("Document '{0}' with key '{1}' does not exist.",
			                          	                                 typeof (TDocument).Name,
			                          	                                 key);
			                          	throw new InvalidOperationException(errorMessage);
			                          },
			                          updateFactory);
		}
	}
}

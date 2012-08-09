using System;

namespace Ziggurat.Infrastructure.Projections
{
	public static class ProjectionWriterExtensions
	{
		public static TView AddOrReplace<TKey, TView>(this IProjectionWriter<TKey, TView> writer, TKey key, Func<TView> addFactory)
		{
			return writer.AddOrUpdate(key,
			                          addFactory,
			                          x => addFactory());
		}

		/// <summary>
		/// Adds the specified view model or replaces an existing one
		/// </summary>
		public static TView AddOrReplace<TKey, TView>(this IProjectionWriter<TKey, TView> writer, TKey key, TView view)
		{
			return writer.AddOrReplace(key, () => view);
		}

		public static TView AddOrUpdate<TKey, TView>(this IProjectionWriter<TKey, TView> writer, TKey key, Func<TView> addFactory, Action<TView> updateFactory)
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
		/// Adds the specified view model or updates the existing one
		/// </summary>
		public static TView AddOrUpdate<TKey, TView>(this IProjectionWriter<TKey, TView> writer, TKey key, TView view, Action<TView> updateFactory)
		{
			return writer.AddOrUpdate(key,
			                          () => view,
			                          x =>
			                          {
			                          	updateFactory(x);
			                          	return x;
			                          });
		}

		/// <summary>
		/// Adds a new view model ot throws an exception if there is one already added
		/// </summary>
		public static TView	Add<TKey, TView>(this IProjectionWriter<TKey, TView> writer, TKey key, TView view)
		{
			return writer.AddOrUpdate(key,
			                          () => view,
			                          x =>
			                          {
			                          	var errorMessage = String.Format("View '{0}' with key '{1}' is already added.",
			                          	                                 typeof (TView).Name,
			                          	                                 key);
			                          	throw new InvalidOperationException(errorMessage);
			                          });
		}

		/// <summary>
		/// Updates a persisted view model or throw an exception if there is no view model persisted for a given key
		/// </summary>
		public static TView Update<TKey, TView>(this IProjectionWriter<TKey, TView> writer, TKey key, Action<TView> updateFactory)
		{
			return writer.AddOrUpdate(key,
			                          () =>
			                          {
			                          	var errorMessage = String.Format("View '{0}' with key '{1}' does not exist.",
			                          	                                 typeof (TView).Name,
			                          	                                 key);
			                          	throw new InvalidOperationException(errorMessage);
			                          },
			                          updateFactory);
		}
	}
}

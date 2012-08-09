using System;

namespace Ziggurat.Infrastructure.Projections
{
	public static class ProjectionReaderExtensions
	{
		public static TView Load<TKey, TView>(this IProjectionReader<TKey, TView> reader, TKey key)
		{
			TView view;
			if (reader.TryGet(key, out view))
				return view;

			var errorMessage = String.Format("View '{0}' with key '{1}' does not exist.", typeof(TView).Name, key);
			throw new InvalidOperationException(errorMessage);
		}
	}
}

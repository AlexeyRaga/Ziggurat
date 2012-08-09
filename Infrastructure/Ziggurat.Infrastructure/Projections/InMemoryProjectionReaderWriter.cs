using System;

namespace Ziggurat.Infrastructure.Projections
{
	public sealed class InMemoryProjectionReaderWriter<TKey, TView> : IProjectionReader<TKey, TView>, IProjectionWriter<TKey, TView>
	{
		public bool TryGet(TKey key, out TView view)
		{
			throw new NotImplementedException();
		}

	    public bool Exists(TKey key)
	    {
	        throw new NotImplementedException();
	    }

	    public TView AddOrUpdate(TKey key, Func<TView> addFactory, Func<TView, TView> updateFactory)
		{
			throw new NotImplementedException();
		}

		public bool TryDelete(TKey key)
		{
			throw new NotImplementedException();
		}
	}
}

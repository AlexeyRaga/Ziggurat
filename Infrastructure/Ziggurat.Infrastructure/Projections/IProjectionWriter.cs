using System;

namespace Ziggurat.Infrastructure.Projections
{
    public interface IProjectionWriter<in TKey, TView>
    {
	    TView AddOrUpdate(TKey key, Func<TView> addFactory, Func<TView, TView> updateFactory);
	    bool TryDelete(TKey key);
        bool Exists(TKey key);
    }
}

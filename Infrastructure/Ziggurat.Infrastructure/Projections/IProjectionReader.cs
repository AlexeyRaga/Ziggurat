namespace Ziggurat.Infrastructure.Projections
{
	public interface IProjectionReader<in TKey, TView>
	{
		bool Exists(TKey key);
		bool TryGet(TKey key, out TView view);
	}
}

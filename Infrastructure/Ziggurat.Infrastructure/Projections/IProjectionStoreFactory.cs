namespace Ziggurat.Infrastructure.Projections
{
	public interface IProjectionStoreFactory
	{
		IProjectionReader<TKey, TView> GetReader<TKey, TView>();
		IProjectionWriter<TKey, TView> GetWriter<TKey, TView>(); 
	}
}

namespace Ziggurat.Infrastructure.Projections
{
	public interface IDataStore
	{
		IProjectionReader<TKey, TView> GetReader<TKey, TView>();
		IProjectionWriter<TKey, TView> GetWriter<TKey, TView>(); 
	}
}

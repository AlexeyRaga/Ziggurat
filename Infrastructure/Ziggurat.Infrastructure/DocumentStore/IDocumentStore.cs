namespace Ziggurat.Infrastructure.DocumentStore
{
	public interface IDocumentStore
	{
		IDocumentReader<TKey, TDocument> GetReader<TKey, TDocument>();
		IDocumentWriter<TKey, TDocument> GetWriter<TKey, TDocument>(); 
	}
}

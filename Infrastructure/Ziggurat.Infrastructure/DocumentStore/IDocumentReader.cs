namespace Ziggurat.Infrastructure.DocumentStore
{
	public interface IDocumentReader<in TKey, TDocument>
	{
		bool Exists(TKey key);
		bool TryGet(TKey key, out TDocument view);
	}
}

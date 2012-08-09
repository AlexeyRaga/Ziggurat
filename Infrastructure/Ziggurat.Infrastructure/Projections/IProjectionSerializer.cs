using System.IO;

namespace Ziggurat.Infrastructure.Projections
{
	public interface IProjectionSerializer
	{
		void Serialize<TView>(TView view, Stream stream);
		TView Deserialize<TView>(Stream stream);
	}
}

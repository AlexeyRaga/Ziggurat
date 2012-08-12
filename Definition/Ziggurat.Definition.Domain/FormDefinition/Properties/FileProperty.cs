using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class FileProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.File; } }
	}
}
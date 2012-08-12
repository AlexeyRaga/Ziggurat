using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class ConcatenationProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.Concatenation; } }
	}
}
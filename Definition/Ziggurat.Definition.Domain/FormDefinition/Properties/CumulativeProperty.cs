using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class CumulativeProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.CumulativeText; } }
	}
}
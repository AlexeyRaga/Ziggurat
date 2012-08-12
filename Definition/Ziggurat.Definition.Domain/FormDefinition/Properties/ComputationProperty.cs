using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class ComputationProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.Computation; } }
	}
}
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class CounterProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.Counter; } }
	}
}
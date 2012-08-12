using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class FloatProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.Float; } }
	}
}

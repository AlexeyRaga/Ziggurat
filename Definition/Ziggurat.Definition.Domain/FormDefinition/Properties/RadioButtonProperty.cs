using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class RadioButtonProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.RadioButton; } }
	}
}
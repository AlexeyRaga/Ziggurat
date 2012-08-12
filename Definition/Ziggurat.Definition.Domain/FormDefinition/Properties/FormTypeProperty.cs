using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class FormTypeProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.FormType; } }
	}
}
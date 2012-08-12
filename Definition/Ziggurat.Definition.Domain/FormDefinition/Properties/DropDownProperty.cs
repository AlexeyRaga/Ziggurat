using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class DropDownProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.DropDownList; } }
	}
}

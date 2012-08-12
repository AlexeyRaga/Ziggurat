using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class FormLinkProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.FormLink; } }
	}
}

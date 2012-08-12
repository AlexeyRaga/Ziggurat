using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class DateProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.Date; } }
	}
}
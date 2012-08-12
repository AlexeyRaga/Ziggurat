using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class DateTimeProperty: PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.DateTime; } }
	}
}
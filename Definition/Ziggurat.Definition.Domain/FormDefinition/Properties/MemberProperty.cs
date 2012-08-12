using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class MemberProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.Member; } }
	}
}
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class ThreadHistoryProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.ThreadHistory; } }
	}
}
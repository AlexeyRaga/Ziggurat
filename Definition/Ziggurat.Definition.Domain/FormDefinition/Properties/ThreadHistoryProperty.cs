using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class ThreadHistoryProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.ThreadHistory; } }

        public ThreadHistoryProperty(FormDefinitionAggregate definition, Guid id, string uniqueName)
            : base(definition, id, uniqueName) { }
	}
}
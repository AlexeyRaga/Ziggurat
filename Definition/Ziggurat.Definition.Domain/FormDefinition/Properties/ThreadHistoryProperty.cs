using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class ThreadHistoryProperty : PropertyBase
	{
        public ThreadHistoryProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}
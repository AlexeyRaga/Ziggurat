using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class CounterProperty : PropertyBase
	{
        public CounterProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}
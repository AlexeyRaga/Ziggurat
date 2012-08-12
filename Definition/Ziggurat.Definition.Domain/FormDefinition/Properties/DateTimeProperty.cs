using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class DateTimeProperty: PropertyBase
	{
        public DateTimeProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}
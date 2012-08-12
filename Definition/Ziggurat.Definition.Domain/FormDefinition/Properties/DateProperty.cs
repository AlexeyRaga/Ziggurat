using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class DateProperty : PropertyBase
	{
        public DateProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}
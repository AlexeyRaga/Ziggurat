using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class DropDownProperty : PropertyBase
	{
        public DropDownProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}

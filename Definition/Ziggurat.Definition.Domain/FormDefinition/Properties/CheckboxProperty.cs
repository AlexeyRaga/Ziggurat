using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class CheckboxProperty : PropertyBase
	{
        public CheckboxProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}

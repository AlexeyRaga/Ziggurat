using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class RadioButtonProperty : PropertyBase
	{
        public RadioButtonProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}
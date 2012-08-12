using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class FormTypeProperty : PropertyBase
	{
        public FormTypeProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}
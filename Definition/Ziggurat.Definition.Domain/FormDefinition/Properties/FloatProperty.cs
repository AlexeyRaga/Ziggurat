using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class FloatProperty : PropertyBase
	{
        public FloatProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}

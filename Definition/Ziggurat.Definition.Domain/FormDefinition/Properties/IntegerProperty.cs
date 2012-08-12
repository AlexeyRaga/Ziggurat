using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
    public sealed class IntegerProperty : PropertyBase
    {
        public override PropertyType Type { get { return PropertyType.Integer; } }

        public IntegerProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
    }
}

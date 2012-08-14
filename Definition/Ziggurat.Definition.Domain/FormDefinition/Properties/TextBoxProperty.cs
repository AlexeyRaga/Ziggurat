using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
    public sealed class TextboxProperty : PropertyBase
    {
        public TextboxProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
    }
}

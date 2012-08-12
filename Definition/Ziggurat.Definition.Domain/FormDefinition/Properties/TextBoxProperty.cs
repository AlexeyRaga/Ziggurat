using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
    public sealed class TextboxProperty : PropertyBase
    {
        public override PropertyType Type { get { return PropertyType.Textbox; } }

        public TextboxProperty(FormDefinitionAggregate definition, Guid id, string uniqueName)
            : base(definition, id, uniqueName) { }
    }
}

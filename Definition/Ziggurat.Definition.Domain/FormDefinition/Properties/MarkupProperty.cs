using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class MarkupProperty : PropertyBase
	{
        public MarkupProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}
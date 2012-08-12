using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class FormLinkProperty : PropertyBase
	{
        public FormLinkProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}

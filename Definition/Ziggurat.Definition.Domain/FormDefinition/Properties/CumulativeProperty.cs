using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class CumulativeProperty : PropertyBase
	{
        public CumulativeProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}
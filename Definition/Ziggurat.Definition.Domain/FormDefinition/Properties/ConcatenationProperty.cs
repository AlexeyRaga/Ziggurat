using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class ConcatenationProperty : PropertyBase
	{
        public ConcatenationProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}
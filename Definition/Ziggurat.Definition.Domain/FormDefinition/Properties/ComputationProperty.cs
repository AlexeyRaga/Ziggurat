using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class ComputationProperty : PropertyBase
	{
        public ComputationProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}
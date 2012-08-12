using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class TimeProperty : PropertyBase
	{
        public TimeProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}
using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class CompanyProperty : PropertyBase
	{
        public CompanyProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}
using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class CompanyProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.Company; } }

        public CompanyProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}
using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class CumulativeProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.CumulativeText; } }

        public CumulativeProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}
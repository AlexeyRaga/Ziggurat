using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class ConcatenationProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.Concatenation; } }

        public ConcatenationProperty(FormDefinitionAggregate definition, Guid id, string uniqueName)
            : base(definition, id, uniqueName) { }
	}
}
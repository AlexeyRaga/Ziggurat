using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class CounterProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.Counter; } }

        public CounterProperty(FormDefinitionAggregate definition, Guid id, string uniqueName)
            : base(definition, id, uniqueName) { }
	}
}
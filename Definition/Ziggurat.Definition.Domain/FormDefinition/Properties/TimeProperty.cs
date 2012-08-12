using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class TimeProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.Time; } }

        public TimeProperty(FormDefinitionAggregate definition, Guid id, string uniqueName)
            : base(definition, id, uniqueName) { }
	}
}
using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class CheckboxProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.CheckBox; } }

        public CheckboxProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}

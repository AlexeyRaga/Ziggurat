using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class FormTypeProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.FormType; } }

        public FormTypeProperty(FormDefinitionAggregate definition, Guid id, string uniqueName)
            : base(definition, id, uniqueName) { }
	}
}
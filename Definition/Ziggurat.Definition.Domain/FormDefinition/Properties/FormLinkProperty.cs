﻿using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class FormLinkProperty : PropertyBase
	{
		public override PropertyType Type { get { return PropertyType.FormLink; } }

        public FormLinkProperty(FormDefinitionAggregate definition, Guid id, string uniqueName)
            : base(definition, id, uniqueName) { }
	}
}

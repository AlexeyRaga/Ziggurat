using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class FileProperty : PropertyBase
	{
        public FileProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}
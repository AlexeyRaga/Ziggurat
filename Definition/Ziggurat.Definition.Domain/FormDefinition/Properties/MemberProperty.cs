using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
	public sealed class MemberProperty : PropertyBase
	{
        public MemberProperty(FormDefinitionAggregate definition, Guid id)
            : base(definition, id) { }
	}
}
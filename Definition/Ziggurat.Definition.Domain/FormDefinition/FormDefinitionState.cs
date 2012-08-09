using System;
using System.Collections.Generic;
using Ziggurat.Contracts;
using Ziggurat.Definition.Domain.Base;
using Ziggurat.Definition.Domain.FormDefinition.Properties;

namespace Ziggurat.Definition.Domain.FormDefinition
{
    public sealed class FormDefinitionState : AggregateState
    {
        public Guid Id { get; set; }
        public IList<PropertyState> Properties { get; private set; }

        public FormDefinitionState()
        {
            Properties = new List<PropertyState>();
        }

        public void When(FormCreated evt)
        {
            Id = evt.Id;
        }
    }
}

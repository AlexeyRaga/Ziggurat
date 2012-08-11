using System;
using System.Collections.Generic;
using System.Dynamic;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
    public sealed class FormDefinitionState : AggregateState
    {
        public Guid Id { get; set; }
        public IList<ExpandoObject> Properties { get; private set; }

        public FormDefinitionState()
        {
            Properties = new List<ExpandoObject>();
        }

        public void When(FormCreated evt)
        {
            Id = evt.Id;
        }

        public void When(PropertyCreated evt)
        {
            dynamic propertyState = new ExpandoObject();
            propertyState.Id = evt.PropertyId;
            propertyState.UniqueName = evt.UniqueName;

            Properties.Add(propertyState);
        }
    }
}

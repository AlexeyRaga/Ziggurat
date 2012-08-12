﻿using System;
using System.Collections.Generic;
using System.Linq;
using Ziggurat.Contracts;
using Ziggurat.Definition.Domain;

namespace Ziggurat.Definition.Domain.FormDefinition
{
    public sealed class FormDefinitionAggregate : AggregateRootBase
    {
        public Guid Id { get; private set; }

        private IList<PropertyBase> _properties = new List<PropertyBase>();

        public void Create(Guid projectId, Guid id, string name, string uniqueName)
        {
            if (Id != Guid.Empty) throw new InvalidOperationException("Form has already been created");
            Apply(new FormCreated(projectId, id, name, uniqueName));
        }

        public void CreateProperty(Guid id, PropertyType type, string name, string uniqueName)
        {
            Apply(new PropertyCreated(Id, id, type, name, uniqueName));
        }

        public void MakePropertyUnused(Guid propertyId)
        {
            var propertyToMakeUnused = _properties.First(x => x.Id == propertyId);
            propertyToMakeUnused.MakeUnused();
        }

        public void MakePropertyUsed(Guid propertyId)
        {
            var propertyToMakeUnused = _properties.First(x => x.Id == propertyId);
            propertyToMakeUnused.MakeUsed();
        }

        public void When(FormCreated evt)
        {
            Id = evt.Id;
        }

        public void When(PropertyCreated evt)
        {
            var property = PropertyFactory.Create(this, evt.PropertyId, evt.Type, evt.UniqueName);
            _properties.Add(property);
        }

        //Redirect all property events to properties
        public void When(IPropertyDefinitionEvent evt)
        {
            var propertyToDeliverEvent = _properties.FirstOrDefault(x => x.Id == evt.PropertyId);
            if (propertyToDeliverEvent != null)
                ((dynamic)propertyToDeliverEvent).When((dynamic)evt);
        }
    }
}

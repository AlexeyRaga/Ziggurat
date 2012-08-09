﻿using System;
using Ziggurat.Contracts;
using Ziggurat.Definition.Domain.Base;

namespace Ziggurat.Definition.Domain.FormDefinition
{
    public sealed class FormDefinitionAggregate : AggregateRootBase<FormDefinitionState>
    {
        public void Create(Guid id, string name, string uniqueName)
        {
            if (State.Id != Guid.Empty) throw new InvalidOperationException("Form has already been created");
            Apply(new FormCreated(id, name, uniqueName));
        }

        public void CreateProperty(Guid id, PropertyType type, string name, string uniqueName)
        {
            Apply(new PropertyCreated(State.Id, id, name, uniqueName));
        }
    }
}

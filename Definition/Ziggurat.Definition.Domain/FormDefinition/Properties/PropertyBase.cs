using System;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.FormDefinition
{
    public abstract class PropertyBase
    {
        public Guid Id { get; set; }

        //do we really care? Isn't it enough to have a derived type like FormPropertyText?
        public abstract PropertyType Type { get; }

        protected bool IsUnused { get; set; }
        protected bool IsNameHidden { get; set; }
        protected bool IsRequired { get; set; }
        protected bool IsReadOnly { get; set; }

        protected FormDefinitionAggregate Definition { get; private set; }
        
        protected PropertyBase(FormDefinitionAggregate definition, Guid id)
        {
            Definition = definition;
            Id = id;
            IsUnused = true;
        }
 
        public void MakeUsed()
        {
            if (!IsUnused) return;
            Definition.Apply(new PropertyMadeUsed(Definition.Id, Id));
        }

        public void MakeUnused()
        {
            if (IsUnused) return;
            Definition.Apply(new PropertyMadeUnused(Definition.Id, Id));
        }

        public void When(PropertyMadeUsed evt) { IsUnused = false; }
        public void When(PropertyMadeUnused evt) { IsUnused = true; }
    }
}

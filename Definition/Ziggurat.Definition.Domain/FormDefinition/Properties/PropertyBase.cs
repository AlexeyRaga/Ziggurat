using System;
using Ziggurat.Contracts.Definition;

namespace Ziggurat.Definition.Domain.FormDefinition
{
    public abstract class PropertyBase
    {
        public Guid Id { get; private set; }

        protected bool IsUnused { get; private set; }
        protected bool IsNameHidden { get; private set; }
        protected bool IsRequired { get; private set; }
        protected bool IsReadOnly { get; private set; }

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

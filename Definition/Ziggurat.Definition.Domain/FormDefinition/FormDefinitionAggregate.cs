using System;
using System.Linq;
using Ziggurat.Contracts;
using Ziggurat.Definition.Domain;
using Ziggurat.Definition.Domain.FormDefinition.Properties;

namespace Ziggurat.Definition.Domain.FormDefinition
{
    public sealed class FormDefinitionAggregate : AggregateRootBase<FormDefinitionState>
    {
        public void Create(Guid projectId, Guid id, string name, string uniqueName)
        {
            if (State.Id != Guid.Empty) throw new InvalidOperationException("Form has already been created");
            Apply(new FormCreated(projectId, id, name, uniqueName));
        }

        public void CreateProperty(Guid id, PropertyType type, string name, string uniqueName)
        {
            Apply(new PropertyCreated(State.Id, id, type, name, uniqueName));
        }

        // I don't know if this is a way to go...
        public PropertyBase GetProperty(Guid id)
        {
            var propertyState = State.Properties
                .Cast<dynamic>()
                .First(x => x.Id == id);

            return PropertyFactory.BuildPropertyEntity(propertyState.Type, propertyState);
        }
    }
}

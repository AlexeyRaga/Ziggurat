using System;
using Ziggurat.Contracts;
using Ziggurat.Infrastructure.EventStore;

namespace Ziggurat.Definition.Domain.FormDefinition
{
    public sealed class FormDefinitionApplicationService : ApplicationServiceBase<FormDefinitionAggregate>
    {
        public FormDefinitionApplicationService(IEventStore store) : base(store) { }

        public void When(CreateForm cmd)
        {
            if (cmd.Id == Guid.Empty) throw new ArgumentException("Form ID is required");
            if (String.IsNullOrWhiteSpace(cmd.Name)) throw new ArgumentException("Name is required");
            if (String.IsNullOrWhiteSpace(cmd.UniqueName)) throw new ArgumentNullException("Unique Name is required");

            Update(cmd.Id, form => form.Create(cmd.ProjectId, cmd.Id, cmd.Name, cmd.UniqueName));
        }

        public void When(CreateProperty cmd)
        {
            if (cmd.FormId == Guid.Empty) throw new ArgumentException("Form ID is required");
            if (String.IsNullOrWhiteSpace(cmd.Name)) throw new ArgumentException("Name is required");

            Update(cmd.FormId, form => form.CreateProperty(cmd.PropertyId, cmd.Type, cmd.Name));
        }

        public void When(MakePropertyUsed cmd)
        {
            if (cmd.FormId == Guid.Empty) throw new ArgumentException("Form ID is required");
            Update(cmd.FormId, form => form.MakePropertyUsed(cmd.PropertyId));
        }

        public void When(MakePropertyUnused cmd)
        {
            if (cmd.FormId == Guid.Empty) throw new ArgumentException("Form ID is required");
            Update(cmd.FormId, form => form.MakePropertyUnused(cmd.PropertyId));
        }
    }
}

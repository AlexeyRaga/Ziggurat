using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ziggurat.Contracts;
using Ziggurat.Infrastructure;

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

            Update(cmd.Id, form => form.Create(cmd.Id, cmd.Name, cmd.UniqueName));
        }
    }
}

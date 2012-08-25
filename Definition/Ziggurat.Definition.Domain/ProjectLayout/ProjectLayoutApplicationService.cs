using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Definition;
using Ziggurat.Infrastructure.EventStore;

namespace Ziggurat.Definition.Domain.ProjectLayout
{
    public sealed class ProjectLayoutApplicationService : ApplicationServiceBase<ProjectLayoutAggregate>
    {
        public ProjectLayoutApplicationService(IEventStore eventStore)
            : base(eventStore)
        {
        }

        public void When(FormAttachedToProjectLayout cmd)
        {
            Update(cmd.ProjectLayoutId, layout => layout.AttachForm(cmd.FormId));
        }
    }
}

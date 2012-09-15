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

        public void When(CreateLayoutForProject cmd)
        {
            Update(cmd.ProjectLayoutId, aggregate => aggregate.CreateForProject(cmd.ProjectLayoutId, cmd.ProjectId));
        }

        public void When(AttachFormToProjectLayout cmd)
        {
            Update(cmd.ProjectLayoutId, aggregate => aggregate.AttachForm(cmd.FormId));
        }

        public void When(MoveFormInProjectLayout cmd)
        {
            Update(cmd.ProjectLayoutId, aggregate => aggregate.MoveFormToPosition(cmd.FormId, cmd.BlockHeader, cmd.Order));
        }
    }
}

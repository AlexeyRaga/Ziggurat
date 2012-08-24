using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;
using Ziggurat.Contracts.Definition;

namespace Ziggurat.Definition.Domain.ProjectLayout
{
    public sealed class ProjectLayoutAggregate : AggregateRootBase<ProjectLayoutState>
    {
        public const string DefaultBlockHeaderName = "Mailbox";

        public void CreateForProject(Guid projectId, Guid id)
        {
            Apply(new ProjectLayoutCreated(projectId, id));
        }

        public void AddForm(Guid guid)
        {
            Apply(new FormAddedToProject(State.ProjectId, State.Id, guid, DefaultBlockHeaderName));
        }
    }
}

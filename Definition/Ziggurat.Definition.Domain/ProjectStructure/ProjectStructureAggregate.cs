using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.ProjectStructure
{
    public sealed class ProjectStructureAggregate : AggregateRootBase<ProjectStructureState>
    {
        public const string DefaultBlockHeaderName = "Mailbox";

        public void CreateForProject(Guid projectId, Guid id)
        {
            Apply(new ProjectStructureCreated(projectId, id));
        }

        public void AddForm(Guid guid)
        {
            Apply(new FormAddedToProject(State.ProjectId, State.Id, guid, DefaultBlockHeaderName));
        }
    }
}

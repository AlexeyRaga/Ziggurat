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
        public void CreateProjectStructure(Guid projectId, Guid id)
        {
            if (State.Created) throw new InvalidOperationException("Project structure is already created");
            Apply(new ProjectStructureCreated(projectId, id));
        }
    }
}

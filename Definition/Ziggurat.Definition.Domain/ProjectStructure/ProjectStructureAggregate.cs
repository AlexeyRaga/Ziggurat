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
        public void CreateForProject(Guid projectId, Guid id)
        {
            Apply(new ProjectStructureCreated(projectId, id));
        }
    }
}

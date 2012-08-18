using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.ProjectStructure
{
    public sealed class ProjectStructureState : State
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }

        public void When(ProjectStructureCreated evt)
        {
            Id = evt.Id;
            ProjectId = evt.ProjectId;
        }
    }
}

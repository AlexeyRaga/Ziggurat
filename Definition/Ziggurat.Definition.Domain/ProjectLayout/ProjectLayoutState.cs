using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.ProjectLayout
{
    public sealed class ProjectLayoutState : State
    {
        public Guid Id { get; set; }
        public Guid ProjectId { get; set; }

        public void When(ProjectLayoutCreated evt)
        {
            Id = evt.Id;
            ProjectId = evt.ProjectId;
        }
    }
}

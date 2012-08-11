using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain.Project
{
    public sealed class ProjectState : State
    {
        public Guid Id { get; set; }

        public void When(ProjectCreated evt)
        {
            Id = evt.Id;
        }
    }
}

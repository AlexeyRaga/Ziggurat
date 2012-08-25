using System;
using System.Collections.Generic;
using System.Linq;
using Ziggurat.Contracts.Definition;

namespace Ziggurat.Definition.Domain.Project
{
    public sealed class ProjectState : State
    {
        public Guid Id { get; set; }
        public Guid LayoutId { get; set; }

        public void When(ProjectCreated evt)
        {
            Id = evt.Id;
            LayoutId = evt.ProjectLayoutId;
        }
    }
}

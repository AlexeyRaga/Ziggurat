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
        public string Name { get; set; }
        public string ShortName { get; set; }

        public void When(NewProjectRegistered evt)
        {
            Id = evt.ProjectId;
            Name = evt.Name;
            ShortName = evt.ShortName;
        }

        public void When(ProjectLayoutAssignedToProject evt)
        {
            LayoutId = evt.ProjectLayoutId;
        }
    }
}

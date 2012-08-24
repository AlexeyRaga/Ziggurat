using System;
using System.Collections.Generic;
using Ziggurat.Contracts.Definition;

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

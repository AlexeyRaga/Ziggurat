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

        public void CreateForProject(Guid projectLayoutId, Guid projectId)
        {
            Apply(new ProjectLayoutCreated(projectId, projectLayoutId));
        }

        public void AttachForm(Guid formId)
        {
            Apply(new FormAttachedToProjectLayout(State.Id, formId, State.ProjectId));
            Apply(new FormMovedInProjectLayout(State.Id, formId, DefaultBlockHeaderName, 0));
        }

        public void MoveFormToPosition(Guid formId, string blockHeader, int position)
        {
            Apply(new FormMovedInProjectLayout(State.Id, formId, blockHeader, position));
        }
    }
}

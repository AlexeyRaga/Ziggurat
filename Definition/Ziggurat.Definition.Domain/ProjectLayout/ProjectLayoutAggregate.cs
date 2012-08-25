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

        public void CreateForProject(Guid projectId, Guid id)
        {
            Apply(new ProjectLayoutCreated(projectId, id));
        }

        public void AttachForm(Guid formId)
        {
            Apply(new FormAttachedToProjectLayout(formId, State.ProjectId, State.Id, DefaultBlockHeaderName, 0));
        }
    }
}

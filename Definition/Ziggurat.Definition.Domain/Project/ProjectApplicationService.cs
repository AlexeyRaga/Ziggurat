using System;
using Ziggurat.Contracts.Definition;
using Ziggurat.Infrastructure.EventStore;

namespace Ziggurat.Definition.Domain.Project
{
    public sealed class ProjectApplicationService : ApplicationServiceBase<ProjectAggregate>
    {
        public ProjectApplicationService(IEventStore eventStore) 
            : base(eventStore) { }

        public void When(CreateNewProject cmd)
        {
            if (cmd.Id == Guid.Empty) throw new ArgumentException("ID is required");
            if (String.IsNullOrWhiteSpace(cmd.Name)) throw new ArgumentException("Project name is required");
            if (String.IsNullOrWhiteSpace(cmd.ShortName)) throw new ArgumentException("Short name is required");

            Update(cmd.Id, prj => prj.Create(cmd.Id, cmd.Name, cmd.ShortName));
        }

        public void When(AssignProjectLayoutToProject cmd)
        {
            Update(cmd.ProjectId, aggregate => aggregate.AssignProjectLayout(cmd.ProjectLayoutId));
        }

        public void When(AddFormToProject cmd)
        {
            Update(cmd.ProjectId, prj => prj.AddForm(cmd.FormId));
        }
    }
}

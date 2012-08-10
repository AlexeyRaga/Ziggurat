using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;
using Ziggurat.Infrastructure.EventStore;

namespace Ziggurat.Definition.Domain.Project
{
    public sealed class ProjectApplicationService : ApplicationServiceBase<ProjectAggregate>
    {
        public ProjectApplicationService(IEventStore eventStore) 
            : base(eventStore) { }

        public void When(CreateProject cmd)
        {
            if (cmd.Id == Guid.Empty) throw new ArgumentException("ID is required");
            if (String.IsNullOrWhiteSpace(cmd.Name)) throw new ArgumentException("Project name is required");
            if (String.IsNullOrWhiteSpace(cmd.ShortName)) throw new ArgumentException("Short name is required");

            Update(cmd.Id, prj => prj.Create(cmd.Id, cmd.Name, cmd.ShortName));
        }
    }
}

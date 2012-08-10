using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Definition.Domain.Base;
using Ziggurat.Infrastructure.EventStore;

namespace Ziggurat.Definition.Domain.Project
{
    public sealed class ProjectApplicationService : ApplicationServiceBase<ProjectAggregate>
    {
        public ProjectApplicationService(IEventStore eventStore) : base(eventStore)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts.Definition;
using Ziggurat.Definition.Domain.Lookups;
using Ziggurat.Infrastructure.EventStore;

namespace Ziggurat.Definition.Domain.ProjectLayout
{
    public sealed class ProjectLayoutApplicationService : ApplicationServiceBase<ProjectLayoutAggregate>
    {
        private readonly IProjectLayoutLookupService _projectStructureLookupService;
        public ProjectLayoutApplicationService(IEventStore eventStore, IProjectLayoutLookupService projectStructureLookupService)
            : base(eventStore)
        {
            _projectStructureLookupService = projectStructureLookupService;
        }

        public void When(AddFormToProject cmd)
        {
            var aggregateId = _projectStructureLookupService.GetStructureIdByProjectId(cmd.ProjectId);

            Update(aggregateId, layout => layout.AddForm(cmd.FormId));
        }
    }
}

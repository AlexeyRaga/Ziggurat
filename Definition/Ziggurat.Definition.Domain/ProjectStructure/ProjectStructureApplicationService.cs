using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;
using Ziggurat.Definition.Domain.Lookups;
using Ziggurat.Infrastructure.EventStore;

namespace Ziggurat.Definition.Domain.ProjectStructure
{
    public sealed class ProjectStructureApplicationService : ApplicationServiceBase<ProjectStructureAggregate>
    {
        private readonly IProjectStructureLookupService _projectStructureLookupService;
        public ProjectStructureApplicationService(IEventStore eventStore, IProjectStructureLookupService projectStructureLookupService)
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

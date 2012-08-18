using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Definition.Domain.Lookups;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.EventStore;
using Ziggurat.Infrastructure.Projections;

namespace Ziggurat.Definition.Domain
{
    public static class DomainBoundedContext
    {
        public static IEnumerable<object> BuildProjections(IProjectionStoreFactory factory)
        {
            yield return new Lookups.ProjectStructureLookupProjection(factory);
            yield break;
        }

        public static IEnumerable<object> BuildApplicationServices(IEventStore eventStore, IProjectionStoreFactory projectionStore)
        {
            yield return new FormDefinition.FormDefinitionApplicationService(eventStore);
            yield return new Project.ProjectApplicationService(eventStore);
            yield return new ProjectLayout.ProjectLayoutApplicationService(eventStore, new ProjectLayoutLookupService(projectionStore));
        }

        public static IEnumerable<object> BuildEventProcessors(IBus bus)
        {
            yield return new Processes.ProjectCreationProcess(bus);
            yield break;
        }
    }
}

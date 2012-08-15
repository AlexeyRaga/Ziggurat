using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.EventStore;

namespace Ziggurat.Definition.Domain
{
    public static class DomainBoundedContext
    {
        public static IEnumerable<object> BuildProjections()
        {
            yield break;
        }

        public static IEnumerable<object> BuildApplicationServices(IEventStore eventStore)
        {
            yield return new FormDefinition.FormDefinitionApplicationService(eventStore);
            yield return new Project.ProjectApplicationService(eventStore);
        }

        public static IEnumerable<object> BuildEventProcessors(IBus bus)
        {
            yield return new ProjectStructure.ProjectStructureDomainService(bus);
        }
    }
}

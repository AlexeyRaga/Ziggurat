using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.EventStore;
using Ziggurat.Infrastructure.DocumentStore;

namespace Ziggurat.Definition.Domain
{
    public static class DomainBoundedContext
    {
        public static IEnumerable<object> BuildProjections(IDocumentStore factory)
        {
            yield break;
        }

        public static IEnumerable<object> BuildApplicationServices(IEventStore eventStore, IDocumentStore projectionStore)
        {
            yield return new FormDefinition.FormDefinitionApplicationService(eventStore);
            yield return new Project.ProjectApplicationService(eventStore);
            yield return new ProjectLayout.ProjectLayoutApplicationService(eventStore);
        }

        public static IEnumerable<object> BuildEventProcessors(ICommandSender commandSender)
        {
            yield return new Processes.ProjectCreationProcess(commandSender);
            yield return new Processes.FormCreationProcess(commandSender);
            yield break;
        }
    }
}

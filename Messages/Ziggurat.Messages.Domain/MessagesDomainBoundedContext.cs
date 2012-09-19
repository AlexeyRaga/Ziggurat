using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.DocumentStore;
using Ziggurat.Infrastructure.EventStore;

namespace Ziggurat.Messages.Domain
{
    public static class MessagesDomainBoundedContext
    {
        public static IEnumerable<object> BuildProjections(IDocumentStore docStore)
        {
            yield break;
        }

        public static IEnumerable<object> BuildApplicationServices(IEventStore eventStore, IDocumentStore docStore)
        {
            yield break;
        }

        public static IEnumerable<object> BuildEventProcessors(ICommandSender commandSender)
        {
            yield break;
        }
    }
}

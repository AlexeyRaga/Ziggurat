using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Client.Setup;
using Ziggurat.Definition.Domain;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.Evel;
using Ziggurat.Infrastructure.EventStore;
using Ziggurat.Infrastructure.Projections;
using Ziggurat.Infrastructure.Queue;
using Ziggurat.Infrastructure.Queue.FileSystem;
using Ziggurat.Infrastructure.Serialization;

namespace Ziggurat.Definition.Service
{
    public class Program
    {
        static readonly IMessageDispatcher EventsDispatcher = new ConventionalToWhenDispatcher();
        static readonly IMessageDispatcher CommandDispatcher = new ConventionalToWhenDispatcher();

        public static void Main(string[] args)
        {
            var commandSender = new SimpleCommandSender(CommandDispatcher);

            var serializer = new JsonValueSerializer();

            var projectionFactory =
                new FileSystemProjectionStoreFactory(
                    ConfigurationManager.AppSettings["projectionsRootFolder"], 
                    serializer);

            var queueFactory = new FileSystemQueueFactory(ConfigurationManager.AppSettings["queuesFolder"]);
            var commandsQueue = queueFactory.CreateReader("cmd-contracts-definition");

            var commandsReceiver = new MessageReceiver(new[] { commandsQueue });

            var commandsDispatcher = new ReceivedMessageDispatcher(
                CommandDispatcher.DispatchToOneAndOnlyOne,
                serializer,
                commandsReceiver);

            using (commandsDispatcher)
            {
                using (var eventStore = EventStoreBuilder.CreateEventStore(DispatchEvents))
                {
                    var appServices = DomainBoundedContext.BuildApplicationServices(eventStore, projectionFactory);
                    var processes = DomainBoundedContext.BuildEventProcessors(commandSender);
                    var projections = ClientBoundedContext.BuildProjections(projectionFactory);

                    foreach (var appService in appServices) CommandDispatcher.Subscribe(appService);
                    foreach (var projection in projections) EventsDispatcher.Subscribe(projection);
                    foreach (var process in processes) EventsDispatcher.Subscribe(process);

                    Console.ReadKey();
                }
            }

        }

        private static void DispatchEvents(IEnumerable<Envelope> events)
        {
            foreach (var evt in events)
            {
                EventsDispatcher.DispatchToAll(evt.Body);
            }
        }
    }
}

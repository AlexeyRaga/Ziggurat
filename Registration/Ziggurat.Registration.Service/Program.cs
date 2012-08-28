using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Client.Setup;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.Evel;
using Ziggurat.Infrastructure.EventStore;
using Ziggurat.Infrastructure.Projections;
using Ziggurat.Infrastructure.Queue;
using Ziggurat.Infrastructure.Queue.FileSystem;
using Ziggurat.Infrastructure.Serialization;
using Ziggurat.Registration.Domain;

namespace Ziggurat.Registration.Service
{
    public class Program
    {
        static readonly IMessageDispatcher EventsDispatcher = new ConventionalToWhenDispatcher();
        static readonly IMessageDispatcher CommandDispatcher = new ConventionalToWhenDispatcher();

        static void Main(string[] args)
        {
            //how things are serialized
            var serializer = new JsonValueSerializer();

            //where all the queues are located
            var queueFactory = new FileSystemQueueFactory(ConfigurationManager.AppSettings["queuesFolder"]);

            //where to send commands: how to put them in the right queue
            var whereToSendCommands = new NamespaceBasedCommandRouter("cmd", queueFactory, serializer);

            //build the projections storage
            var projectionStorage =
                new FileSystemProjectionStoreFactory(
                    ConfigurationManager.AppSettings["projectionsRootFolder"],
                    serializer);

            //this BC only receives commands for registrations domain
            var whereToReceiveCommands = queueFactory.CreateReader("cmd-contracts-registration");

            //spin up a commands receiver, it will receive commands and dispatch them to the CommandDispatcher
            var commandsReceiver = new ReceivedMessageDispatcher(
                dispatchTo: CommandDispatcher.DispatchToOneAndOnlyOne,
                serializer: serializer,
                receiver: new MessageReceiver(new[] { whereToReceiveCommands }));

            //start receiving commands
            using (commandsReceiver)
            {
                using (var eventStore = EventStoreBuilder.CreateEventStore(DispatchEvents))
                {
                    var appServices = RegistrationDomainBoundedContext.BuildApplicationServices(eventStore, projectionStorage);
                    var processes = RegistrationDomainBoundedContext.BuildEventProcessors(whereToSendCommands);
                    var projections = RegistrationDomainBoundedContext.BuildProjections(projectionStorage);

                    foreach (var appService in appServices) CommandDispatcher.Subscribe(appService);
                    foreach (var projection in projections) EventsDispatcher.Subscribe(projection);
                    foreach (var process in processes) EventsDispatcher.Subscribe(process);

                    commandsReceiver.Run();
                    Console.ReadKey();
                }
            }
        }

        private static void DispatchCommand(object command)
        {
            using (Colorize.With(ConsoleColor.Green))
            {
                Console.WriteLine(command.ToString());
            }
            CommandDispatcher.DispatchToOneAndOnlyOne(command);
        }

        private static void DispatchEvents(IEnumerable<Envelope> events)
        {
            foreach (var evt in events)
            {
                using (Colorize.With(ConsoleColor.Yellow))
                {
                    Console.WriteLine(evt.ToString());
                }
                EventsDispatcher.DispatchToAll(evt.Body);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ziggurat.Client.Setup;
using Ziggurat.Client.Setup.ProjectionRebuilder;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.EventStore;
using Ziggurat.Infrastructure.Projections;
using Ziggurat.Infrastructure.Queue;
using Ziggurat.Infrastructure.Queue.FileSystem;
using Ziggurat.Infrastructure.Serialization;
using Ziggurat.Registration.Client;
using Ziggurat.Registration.Domain;

namespace Ziggurat.Registration.Service
{
    public class Program
    {
        const string IncommingCommandsQueue = "cmd-contracts-registration";
        const string IncommingEventsQueue = "evt-registration";

        static readonly IMessageDispatcher EventsDispatcher = new ConventionalToWhenDispatcher();
        static readonly IMessageDispatcher CommandDispatcher = new ConventionalToWhenDispatcher();

        static void Main(string[] args)
        {
            Console.WriteLine("Starting in thread: {0}", Thread.CurrentThread.ManagedThreadId); 
            var config = LocalConfig.CreateNew(ConfigurationManager.AppSettings["fileStore"]);

            //where to send commands: this command sender is used by "processes" (things that receive events and
			//publish commands). It makes sense to do it "locally", avoiding any queues.
			//So it would look: got an event -> produced a command -> dispatched/executed it immediately.
	        var whereToSendLocalCommands = new ToDispatcherCommandSender(CommandDispatcher);

            //spin up a commands receiver, it will receive commands and dispatch them to the CommandDispatcher
            var commandsReceiver = config.CreateIncomingMessagesDispatcher(IncommingCommandsQueue, DispatchCommand);
            var eventsReceiver = config.CreateIncomingMessagesDispatcher(IncommingEventsQueue, x => DispatchEvent((Envelope)x));

            using (var host = new Host())
            {
                using (var eventStore = config.CreateEventStore("ZigguratES"))
                {
                    var eventsFromEventSourceToQueueDistributor =
                        new EventStoreToQueueDistributor(IncommingEventsQueue, config.QueueFactory, eventStore, config.ProjectionsStore, config.Serializer);

                    var appServices = RegistrationDomainBoundedContext.BuildApplicationServices(eventStore, config.ProjectionsStore);
                    var processes = RegistrationDomainBoundedContext.BuildEventProcessors(whereToSendLocalCommands);

                    Func<IProjectionStoreFactory, IEnumerable<object>> getProjectionsFunction =
                        factory =>
                        {
                            var domainProjections = RegistrationDomainBoundedContext.BuildProjections(factory);
                            var clientProjections = RegistrationClientBoundedContext.BuildProjections(factory);
                            return domainProjections.Concat(clientProjections);
                        };

                    var projectionRebuilder = new Rebuilder(eventStore, config.ProjectionsStore, getProjectionsFunction);

                    host.AddStartupTask(c => projectionRebuilder.Run());

                    var projections = getProjectionsFunction(config.ProjectionsStore);

                    foreach (var appService in appServices) CommandDispatcher.Subscribe(appService);
                    foreach (var projection in projections) EventsDispatcher.Subscribe(projection);
                    foreach (var process in processes) EventsDispatcher.Subscribe(process);

                    host.AddTask(c => commandsReceiver.Run(c));
                    host.AddTask(c => eventsFromEventSourceToQueueDistributor.Run(c));
                    host.AddTask(c => eventsReceiver.Run(c));
                    host.Run();

                    Thread.Sleep(400);
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

        public static void DispatchEvent(Envelope evt)
        {
            using (Colorize.With(ConsoleColor.Yellow))
            {
                Console.WriteLine(evt.Body.ToString());
            }
            EventsDispatcher.DispatchToAll(evt.Body);
        }
    }
}

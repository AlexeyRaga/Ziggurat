using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ziggurat.Client.Setup;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.Evel;
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
            var commandsReceiver = config.CreateIncomingCommandsDispatcher(IncommingCommandsQueue, DispatchCommand);

            using (var host = new Host())
            {
                using (var eventStore = EventStoreBuilder.CreateEventStore(DispatchEvents))
                {
                    var appServices = RegistrationDomainBoundedContext.BuildApplicationServices(eventStore, config.ProjectionsStore);
                    var processes = RegistrationDomainBoundedContext.BuildEventProcessors(whereToSendLocalCommands);
                    
                    var domainProjections = RegistrationDomainBoundedContext.BuildProjections(config.ProjectionsStore);
                    var clientProjections = RegistrationClientBoundedContext.BuildProjections(config.ProjectionsStore);
                    var projections = domainProjections.Concat(clientProjections);

                    foreach (var appService in appServices) CommandDispatcher.Subscribe(appService);
                    foreach (var projection in projections) EventsDispatcher.Subscribe(projection);
                    foreach (var process in processes) EventsDispatcher.Subscribe(process);

                    host.AddTask(c => commandsReceiver.Run(c));
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

        private static void DispatchEvents(IEnumerable<Envelope> events)
        {
            foreach (var evt in events)
            {
                using (Colorize.With(ConsoleColor.Yellow))
                {
                    Console.WriteLine(evt.Body.ToString());
                }
                EventsDispatcher.DispatchToAll(evt.Body);
            }
        }
    }
}

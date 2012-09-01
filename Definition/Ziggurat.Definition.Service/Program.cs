using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
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
        const string IncommingCommandsQueue = "cmd-contracts-definition";
        static readonly IMessageDispatcher EventsDispatcher = new ConventionalToWhenDispatcher();
        static readonly IMessageDispatcher CommandDispatcher = new ConventionalToWhenDispatcher();

        public static void Main(string[] args)
        {
            Console.WriteLine("Starting in thread: {0}", Thread.CurrentThread.ManagedThreadId); 
            var config = LocalConfig.CreateNew(ConfigurationManager.AppSettings["fileStore"]);

            //where to send commands: this command sender is used by "processes" (things that receive events and
            //publish commands). It makes sense to do it "locally", avoiding any queues.
            //So it would look: got an event -> produced a command -> dispatched/executed it immediately.
            var whereToSendLocalCommands = new ToDispatcherCommandSender(CommandDispatcher);

            //spin up a commands receiver, it will receive commands and dispatch them to the CommandDispatcher
            var commandsReceiver = config.CreateIncomingMessagesDispatcher(IncommingCommandsQueue, DispatchCommand);

            using (var host = new Host())
            {
                using (var eventStore = EventStoreBuilder.CreateEventStore(DispatchEvents))
                {
                    var appServices = DomainBoundedContext.BuildApplicationServices(eventStore, config.ProjectionsStore);
                    var processes = DomainBoundedContext.BuildEventProcessors(whereToSendLocalCommands);
                    var projections = ClientBoundedContext.BuildProjections(config.ProjectionsStore);

                    foreach (var appService in appServices) CommandDispatcher.Subscribe(appService);
                    foreach (var projection in projections) EventsDispatcher.Subscribe(projection);
                    foreach (var process in processes) EventsDispatcher.Subscribe(process);

                    host.AddTask(c => Task.Factory.StartNew(() =>
                    {
                        Console.WriteLine("Run receiver in thread id: {0}", Thread.CurrentThread.ManagedThreadId);
                        commandsReceiver.Run(c);
                    }));

                    host.Run();
                    Thread.Sleep(400);
                    Console.ReadKey();
                }
            }

        }

        private static void DispatchCommand(object command)
        {
            Console.WriteLine(command.ToString());
            CommandDispatcher.DispatchToOneAndOnlyOne(command);
        }

        private static void DispatchEvents(IEnumerable<Envelope> events)
        {
            foreach (var evt in events)
            {
                Console.WriteLine(evt.Body.ToString());
                EventsDispatcher.DispatchToAll(evt.Body);
            }
        }
    }
}

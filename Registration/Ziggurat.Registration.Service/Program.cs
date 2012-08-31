﻿using System;
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
        static readonly IMessageDispatcher EventsDispatcher = new ConventionalToWhenDispatcher();
        static readonly IMessageDispatcher CommandDispatcher = new ConventionalToWhenDispatcher();

        static void Main(string[] args)
        {
            Console.WriteLine("Run service, thread id: {0}", Thread.CurrentThread.ManagedThreadId);
            //how things are serialized
            var serializer = new JsonValueSerializer();

            //where all the queues are located
            var queueFactory = new FileSystemQueueFactory(ConfigurationManager.AppSettings["queuesFolder"]);

            //where to send commands: this command sender is used by "processes" (things that receive events and
			//publish commands). It makes sense to do it "locally", avoiding any queues.
			//So it would look: got an event -> produced a command -> dispatched/executed it immediately.
	        var whereToSendLocalCommands = new ToDispatcherCommandSender(CommandDispatcher);

            //build the projections storage
            var projectionStorage =
                new FileSystemProjectionStoreFactory(
                    ConfigurationManager.AppSettings["projectionsRootFolder"],
                    serializer);

            //this BC only receives commands for registrations domain
            var whereToReceiveCommands = queueFactory.CreateReader("cmd-contracts-registration");

            //spin up a commands receiver, it will receive commands and dispatch them to the CommandDispatcher
            var commandsReceiver = new ReceivedMessageDispatcher(
                dispatchTo: DispatchCommand,
                serializer: serializer,
                receiver: new IncomingMessagesStream(new[] { whereToReceiveCommands }));

            using (var host = new Host())
            {
                using (var eventStore = EventStoreBuilder.CreateEventStore(DispatchEvents))
                {
                    var appServices = RegistrationDomainBoundedContext.BuildApplicationServices(eventStore, projectionStorage);
                    var processes = RegistrationDomainBoundedContext.BuildEventProcessors(whereToSendLocalCommands);
                    
                    var domainProjections = RegistrationDomainBoundedContext.BuildProjections(projectionStorage);
                    var clientProjections = RegistrationClientBoundedContext.BuildProjections(projectionStorage);
                    var projections = domainProjections.Concat(clientProjections);

                    foreach (var appService in appServices) CommandDispatcher.Subscribe(appService);
                    foreach (var projection in projections) EventsDispatcher.Subscribe(projection);
                    foreach (var process in processes) EventsDispatcher.Subscribe(process);

                    host.AddTask(c => Task.Factory.StartNew(() =>
                    {
                        Console.WriteLine("Run receiver, thread id: {0}", Thread.CurrentThread.ManagedThreadId);
                        commandsReceiver.Run(c);
                    }));
                    host.Run();
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

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Ziggurat.Client.Setup;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.Evel;
using Ziggurat.Infrastructure.EventStore;
using Ziggurat.Infrastructure.Projections;

namespace Ziggurat.WebHost
{
    public static class Client
    {
        private static readonly IMessageDispatcher _commandDispatcher = new ConventionalToWhenDispatcher();
        private static readonly IMessageDispatcher _eventsDispatcher = new ConventionalToWhenDispatcher();

        public static ICommandSender CommandSender { get; private set; }

        static Client()
        {
            var projectionStore = new FileSystemProjectionStoreFactory(
                ConfigurationManager.AppSettings["projectionsRootFolder"],
                new JsonProjectionSerializer());

            CommandSender = new SimpleCommandSender(_commandDispatcher);
        }

        private static void DispatchEvents(IEnumerable<Envelope> events)
        {
            foreach (var evt in events)
            {
                _eventsDispatcher.DispatchToAll(evt.Body);
            }
        }

        static void Shutdown()
        {

        }
    }
}
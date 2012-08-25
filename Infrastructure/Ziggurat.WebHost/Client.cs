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
using Ziggurat.Infrastructure.Queue;
using Ziggurat.Infrastructure.Queue.FileSystem;
using Ziggurat.Infrastructure.Serialization;

namespace Ziggurat.WebHost
{
    public static class Client
    {
        private static readonly IMessageDispatcher _commandDispatcher = new ConventionalToWhenDispatcher();
        private static readonly IMessageDispatcher _eventsDispatcher = new ConventionalToWhenDispatcher();

        public static ICommandSender CommandSender { get; private set; }
        public static IViewModelReader ViewModelReader { get; private set; }

        static Client()
        {
            var serializer = new JsonValueSerializer();
            var projectionStore = new FileSystemProjectionStoreFactory(
                ConfigurationManager.AppSettings["projectionsRootFolder"],
                serializer);

            var queueFactory = new FileSystemQueueFactory(ConfigurationManager.AppSettings["queuesFolder"]);

            CommandSender = new NamespaceBasedCommandRouter("cmd", queueFactory, serializer);
            ViewModelReader = new SimpleProjectionReader(projectionStore);
        }
    }
}
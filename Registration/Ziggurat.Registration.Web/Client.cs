using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Ziggurat.Client.Setup;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.Evel;
using Ziggurat.Infrastructure.Projections;
using Ziggurat.Infrastructure.Queue.FileSystem;
using Ziggurat.Infrastructure.Serialization;

namespace Ziggurat.Registration.Web
{
    public static class Client
    {
        private static readonly IMessageDispatcher _commandDispatcher = new ConventionalToWhenDispatcher();
        private static readonly IMessageDispatcher _eventsDispatcher = new ConventionalToWhenDispatcher();

        public static ICommandSender CommandSender { get; private set; }
        public static IViewModelReader ViewModelReader { get; private set; }

        static Client()
        {
            var config = LocalConfig.CreateNew(ConfigurationManager.AppSettings["fileStore"]);

            CommandSender = config.CreateCommandSender();
            
            ViewModelReader = new SimpleProjectionReader(config.ProjectionsStore);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using Ziggurat.Client.Setup;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.DocumentStore;
using Ziggurat.Infrastructure.Queue.FileSystem;
using Ziggurat.Infrastructure.Serialization;

namespace Ziggurat.Web
{
    public static class Client
    {
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
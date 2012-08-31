using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.EventStore;
using Ziggurat.Infrastructure.Projections;
using Ziggurat.Infrastructure.Queue;
using Ziggurat.Infrastructure.Queue.FileSystem;
using Ziggurat.Infrastructure.Serialization;

namespace Ziggurat.Client.Setup
{
    public sealed class Config
    {
        private readonly string _queuesFolder;
        private readonly string _projectionsFolder;
        private readonly ISerializer _serializer;
        private readonly IProjectionStoreFactory _projectionsStore;
        private readonly IQueueFactory _queueFactory;

        public ISerializer Serializer { get { return _serializer; } }
        public IProjectionStoreFactory ProjectionsStore { get { return _projectionsStore; } }

        private Config(string queuesFolder, string projectionsFolder)
        {
            _queuesFolder      = queuesFolder;
            _projectionsFolder = projectionsFolder;
            _serializer        = new JsonValueSerializer();
            _projectionsStore  = new FileSystemProjectionStoreFactory(projectionsFolder, _serializer);
            _queueFactory      = new FileSystemQueueFactory(queuesFolder);
        }

        public IEventStore CreateEventStore(string storeName, Action<IEnumerable<Envelope>> howToDispatchEvents = null)
        {
            if (howToDispatchEvents == null) howToDispatchEvents = evts => {};
            var eventStore = EventStoreBuilder.CreateEventStore(howToDispatchEvents);

            return eventStore;
        }

        public ICommandSender CreateCommandSender()
        {
            return new NamespaceBasedCommandRouter("cmd", _queueFactory, _serializer);
        }

        public ReceivedMessageDispatcher CreateIncomingCommandsDispatcher(string streamName, Action<object> whereToDispatchCommands)
        {
            if (String.IsNullOrWhiteSpace(streamName)) throw new ArgumentNullException("streamName");
            if (whereToDispatchCommands == null) throw new ArgumentNullException("whereToDispatchCommands");

            var incommingCommandsStream = CreateIncomingStream(streamName);

            //spin up a commands receiver, it will receive commands and dispatch them to the CommandDispatcher
            var commandsReceiver = new ReceivedMessageDispatcher(
                dispatchTo: whereToDispatchCommands,
                serializer: _serializer,
                receiver: incommingCommandsStream);

            return commandsReceiver;
        }

        public IncomingMessagesStream CreateIncomingStream(string streamName)
        {
            var queueReader    = _queueFactory.CreateReader(streamName);
            var incomingStream = new IncomingMessagesStream(new[] { queueReader });

            return incomingStream;
        }

        public static Config CreateNew(string rootFolder)
        {
            EnsureFolder(rootFolder);

            var queuesFolder      = CreateSubfolder(rootFolder, "Queues");
            var projectionsFolder = CreateSubfolder(rootFolder, "Views");

            return new Config(queuesFolder, projectionsFolder);
        }

        private static void EnsureFolder(string folder)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);            
        }

        private static string CreateSubfolder(string rootFolder, string subfolderName)
        {
            var path = Path.Combine(rootFolder, subfolderName);
            EnsureFolder(path);
            return path;
        }
    }
}

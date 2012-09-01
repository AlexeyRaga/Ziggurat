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
    public sealed class LocalConfig : Ziggurat.Client.Setup.IConfig
    {
        private readonly string _queuesFolder;
        private readonly string _projectionsFolder;
        private readonly ISerializer _serializer;
        private readonly IProjectionStoreFactory _projectionsStore;
        private readonly IQueueFactory _queueFactory;

        public ISerializer Serializer { get { return _serializer; } }
        public IProjectionStoreFactory ProjectionsStore { get { return _projectionsStore; } }
        public IQueueFactory QueueFactory { get { return _queueFactory; } }

        private LocalConfig(string queuesFolder, string projectionsFolder)
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

        public ReceivedMessageDispatcher CreateIncomingMessagesDispatcher(string streamName, Action<object> whereToDispatch)
        {
            if (String.IsNullOrWhiteSpace(streamName)) throw new ArgumentNullException("streamName");
            if (whereToDispatch == null) throw new ArgumentNullException("whereToDispatch");

            var incommingStream = CreateIncomingStream(streamName);

            //spin up a messages receiver, it will receive messages and push them to the dispatcher
            var commandsReceiver = new ReceivedMessageDispatcher(
                dispatchTo: whereToDispatch,
                serializer: _serializer,
                receiver: incommingStream);

            return commandsReceiver;
        }

        public IncomingMessagesStream CreateIncomingStream(string streamName)
        {
            var queueReader    = _queueFactory.CreateReader(streamName);
            var incomingStream = new IncomingMessagesStream(new[] { queueReader });

            return incomingStream;
        }

        public static LocalConfig CreateNew(string rootFolder)
        {
            EnsureFolder(rootFolder);

            var queuesFolder      = CreateSubfolder(rootFolder, "Queues");
            var projectionsFolder = CreateSubfolder(rootFolder, "Views");

            return new LocalConfig(queuesFolder, projectionsFolder);
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

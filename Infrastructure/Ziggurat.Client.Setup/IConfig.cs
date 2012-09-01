using System;
using System.Collections.Generic;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.EventStore;
using Ziggurat.Infrastructure.Projections;
using Ziggurat.Infrastructure.Queue;
using Ziggurat.Infrastructure.Serialization;
namespace Ziggurat.Client.Setup
{
    public interface IConfig
    {
        ICommandSender CreateCommandSender();
        IEventStore CreateEventStore(string storeName, Action<IEnumerable<Envelope>> howToDispatchEvents = null);
        ReceivedMessageDispatcher CreateIncomingMessagesDispatcher(string streamName, Action<object> whereToDispatch);
        IProjectionStoreFactory ProjectionsStore { get; }
        IQueueFactory QueueFactory { get; }
        ISerializer Serializer { get; }
    }
}

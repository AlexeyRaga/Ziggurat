using System;
using System.Collections.Generic;

namespace Ziggurat.Infrastructure.EventStore
{
    public interface IEventStore : IDisposable
    {
        EventStream Load(Guid aggregateIdentity, int revision);

        void Append(Guid aggregateIdentity, int revision, Guid commitId, IEnumerable<object> events);
    }

    public static class EventStoreExtensions
    {
        public static void Append(this IEventStore store, Guid aggregateIdentity, int revision, IEnumerable<object> events)
        {
            if (store == null) throw new ArgumentNullException("store");
            store.Append(aggregateIdentity, revision, Guid.NewGuid(), events);
        }

        public static EventStream LoadAll(this IEventStore store, Guid aggregateIdentity)
        {
            if (store == null) throw new ArgumentNullException("store");
            return store.Load(aggregateIdentity, 0);
        }
    }
}

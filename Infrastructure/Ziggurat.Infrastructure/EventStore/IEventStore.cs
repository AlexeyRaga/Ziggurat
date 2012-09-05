using System;
using System.Collections.Generic;

namespace Ziggurat.Infrastructure.EventStore
{
    /// <summary>
    /// Event Store abstraction
    /// </summary>
    public interface IEventStore : IDisposable
    {
        /// <summary>
        /// Loads an event stream for the specified aggregate.
        /// </summary>
        /// <param name="aggregateIdentity">An aggregate id</param>
        /// <param name="revision">A revision number to start with (use 0 to load all the events)</param>
        EventStream Load(Guid aggregateIdentity, int revision);

        /// <summary>
        /// Loads all the events since the specified stamp.
        /// </summary>
        IEnumerable<Envelope> LoadSince(long stamp);

        /// <summary>
        /// Appends events to the stream (makes a logical commit).
        /// </summary>
        /// <param name="aggregateIdentity">An aggregate id</param>
        /// <param name="revision">A revision the aggregate is expected to be up to</param>
        /// <param name="commitId">A commit id</param>
        /// <param name="events">A list of events to commit</param>
        void Append(Guid aggregateIdentity, int revision, Guid commitId, IEnumerable<Envelope> events);
    }

    public static class EventStoreExtensions
    {
        /// <summary>
        /// Appends events to the stream (makes a logical commit).
        /// Automatically generates the unique commit ID.
        /// </summary>
        /// <param name="store">An event store (cannot be null)</param>
        /// <param name="aggregateIdentity">An aggregate id</param>
        /// <param name="revision">A revision the aggregate is expected to be up to</param>
        /// <param name="commitId">A commit id</param>
        /// <param name="events">A list of events to commit</param>
        /// <exception cref="T:ArgumentNullException">When store is null</exception>
        public static void Append(this IEventStore store, Guid aggregateIdentity, int revision, IEnumerable<Envelope> events)
        {
            if (store == null) throw new ArgumentNullException("store");
            store.Append(aggregateIdentity, revision, Guid.NewGuid(), events);
        }

        /// <summary>
        /// Loads all the events for the specified aggregate.
        /// </summary>
        /// <param name="store">An event store (cannot be null)</param>
        /// <param name="aggregateIdentity">An aggregate id</param>
        /// <exception cref="T:ArgumentNullException">When store is null</exception>
        public static EventStream LoadAll(this IEventStore store, Guid aggregateIdentity)
        {
            if (store == null) throw new ArgumentNullException("store");
            return store.Load(aggregateIdentity, 0);
        }
    }
}

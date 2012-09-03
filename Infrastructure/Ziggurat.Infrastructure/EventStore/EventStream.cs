using System.Collections.Generic;
using System.Linq;

namespace Ziggurat.Infrastructure.EventStore
{
    /// <summary>
    /// Not a .NET stream, but still a stream :)
    /// Represents a stream of events for the particular aggregate.
    /// </summary>
    public sealed class EventStream
    {
        /// <summary>
        /// A revision number this stream is up to.
        /// </summary>
        public int Revision { get; private set; }

        /// <summary>
        /// The content of the stream: events.
        /// </summary>
        public ICollection<Envelope> Events { get; private set; }

        public EventStream(int revision, IEnumerable<Envelope> events)
        {
            Revision = revision;
            Events = events.ToList();
        }
    }
}

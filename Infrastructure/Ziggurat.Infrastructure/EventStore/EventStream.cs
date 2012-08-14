using System.Collections.Generic;
using System.Linq;

namespace Ziggurat.Infrastructure.EventStore
{
    public sealed class EventStream
    {
        public int Revision { get; private set; }
        public ICollection<Envelope> Events { get; private set; }

        public EventStream(int revision, IEnumerable<Envelope> events)
        {
            Revision = revision;
            Events = events.ToList();
        }
    }
}

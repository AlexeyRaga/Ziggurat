using System.Collections.Generic;
using System.Linq;

namespace Ziggurat.Infrastructure
{
    public sealed class EventStream
    {
        public int Revision { get; private set; }
        public ICollection<object> Events { get; private set; }

        public EventStream(int revision, IEnumerable<object> events)
        {
            Revision = revision;
            Events = events.ToList();
        }
    }
}

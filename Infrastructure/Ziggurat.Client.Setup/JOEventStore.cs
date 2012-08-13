using System;
using System.Linq;
using System.Collections.Generic;
using EventStore;
using Ziggurat.Infrastructure.EventStore;

namespace Ziggurat.Client.Setup
{
    public sealed class JOEventStore : IEventStore
    {
        private readonly IStoreEvents _realEventStore;

        public JOEventStore(IStoreEvents realEventStore)
        {
            if (realEventStore == null) throw new ArgumentNullException("realEventStore");

            _realEventStore = realEventStore;
        }

        public EventStream Load(Guid aggregateIdentity, int revision)
        {
            if (aggregateIdentity == null) throw new ArgumentNullException("aggregateIdentity");

            using (var stream = _realEventStore.OpenStream(aggregateIdentity, revision, int.MaxValue))
            {
                return new EventStream(stream.StreamRevision, stream.CommittedEvents.ToEnvelopes());
            }
        }

        public void Append(Guid aggregateIdentity, int revision, Guid commitId, IEnumerable<Envelope> events)
        {
            if (aggregateIdentity == null) throw new ArgumentNullException("aggregateIdentity");
            if (events == null) return;

            var evtMessages = events.ToEventMessages();

            using (var stream = _realEventStore.OpenStream(aggregateIdentity, revision, int.MaxValue))
            {
                foreach (var evt in evtMessages) stream.Add(evt);
                stream.CommitChanges(commitId);
            }
        }

        public void Dispose()
        {
            _realEventStore.Dispose();
        }
    }

    public static class EnvelopeExtensions
    {
        public static IEnumerable<Envelope> ToEnvelopes(this IEnumerable<EventMessage> messages)
        {
            foreach (var msg in messages)
                yield return new Envelope(msg.Body, msg.Headers);
        }

        public static IEnumerable<EventMessage> ToEventMessages(this IEnumerable<Envelope> events)
        {
            foreach (var evt in events)
            {
                var msg = new EventMessage { Body = evt };

                //populate message headers: extra information for audit and history reasons.
                FillInEnvelopeSpecificHeaders(msg, evt);

                yield return msg;
            }
        }

        private static void FillInEnvelopeSpecificHeaders(EventMessage msg, Envelope evt)
        {
            foreach (var header in evt.Headers)
                msg.Headers[header.Key] = header.Value;
        }
    }
}

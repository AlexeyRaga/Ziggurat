using System;
using System.Linq;
using System.Collections.Generic;
using EventStore;
using Ziggurat.Infrastructure.UserContext;

namespace Ziggurat.Infrastructure.EventStore
{
    public sealed class JOEventStore : IEventStore
    {
        private readonly IStoreEvents _realEventStore;
        private readonly IUserContextProvider _userContextProvider;

        public JOEventStore(IStoreEvents realEventStore, IUserContextProvider userContextProvider)
        {
            if (realEventStore == null) throw new ArgumentNullException("realEventStore");
            if (userContextProvider == null) throw new ArgumentNullException("userContextProvider");

            _realEventStore = realEventStore;
            _userContextProvider = userContextProvider;
        }

        public EventStream Load(Guid aggregateIdentity, int revision)
        {
            if (aggregateIdentity == null) throw new ArgumentNullException("aggregateIdentity");

            using (var stream = _realEventStore.OpenStream(aggregateIdentity, revision, int.MaxValue))
            {
                return new EventStream(stream.StreamRevision, ConvertFromEventMessages(stream.CommittedEvents));
            }
        }

        public void Append(Guid aggregateIdentity, int revision, Guid commitId, IEnumerable<Envelope> events)
        {
            if (aggregateIdentity == null) throw new ArgumentNullException("aggregateIdentity");
            if (events == null) return;

            var userContext = _userContextProvider.GetCurrentContext();

            var evtMessages = ConvertToEventMessages(events);

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

        private static IEnumerable<Envelope> ConvertFromEventMessages(IEnumerable<EventMessage> messages)
        {
            foreach (var msg in messages)
            {
                var envelope = new Envelope(msg.Body, msg.Headers);
                yield return envelope;
            }
        }

        private static IEnumerable<EventMessage> ConvertToEventMessages(IEnumerable<Envelope> events)
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

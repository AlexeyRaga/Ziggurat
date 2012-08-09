using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventStore;

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
                return new EventStream(stream.StreamRevision, stream.CommittedEvents.Select(x=>x.Body));
            }
        }

        public void Append(Guid aggregateIdentity, int revision, Guid commitId, IEnumerable<object> events)
        {
            if (aggregateIdentity == null) throw new ArgumentNullException("aggregateIdentity");
            if (events == null) return;

            var userContext = _userContextProvider.GetCurrentContext();

            var evtMessages = ConvertToEventMessages(events, userContext);

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

        private static IEnumerable<EventMessage> ConvertToEventMessages(IEnumerable<object> events, IUserContext userContext)
        {
            foreach (var evt in events)
            {
                var msg = new EventMessage { Body = evt };

                //populate message headers: extra information for audit and history reasons.
                FillInGenericHeaders(msg);
                FillInUserContextHeaders(msg, userContext);

                yield return msg;
            }
        }

        private static void FillInGenericHeaders(EventMessage msg)
        {
            msg.Headers.Add(EventHeaderKeys.DateCreated, Now.UtcTime);
        }

        private static void FillInUserContextHeaders(EventMessage msg, IUserContext userContext)
        {
            if (userContext == null) return;

            msg.Headers.Add(EventHeaderKeys.MemberId, userContext.MemberId);
        }

    }
}

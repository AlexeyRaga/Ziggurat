using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.EventStore;

namespace Ziggurat.Definition.Domain
{
    public interface IAggregateRepository
    {
        TAggregate GetById<TAggregate>(Guid aggregate) where TAggregate : IAggregateRoot, new();
    }

    public sealed class CommonRepository : IAggregateRepository
    {
        private readonly IEventStore _eventStore;
        public CommonRepository(IEventStore eventStore)
        {
            if (eventStore == null) throw new ArgumentNullException("eventStore");
            _eventStore = eventStore;
        }

        public TAggregate GetById<TAggregate>(Guid aggregateId)
            where TAggregate : IAggregateRoot, new()
        {
            var events = _eventStore.LoadAll(aggregateId);

            var aggregate = new TAggregate
            {
                Id       = aggregateId,
                Revision = events.Revision
            };
            
            aggregate.ApplyFromHistory(events.Events.Select(UnwrapFromEnvelope));

            return aggregate;
        }

        public void Save(IAggregateRoot aggregate)
        {
            _eventStore.Append(
                aggregate.Id, 
                aggregate.Revision, 
                aggregate.Changes.Select(x => WrapIntoEnvelope(aggregate.Id, x)));
        }

        private Envelope WrapIntoEnvelope(Guid aggregateId, IEvent evt)
        {
            var envelope = new Envelope(evt, null);
            envelope.SetAggregateId(aggregateId);
            envelope.SetDateCreated(Now.UtcTime);

            return envelope;
        }

        private IEvent UnwrapFromEnvelope(Envelope env)
        {
            return (IEvent)env.Body;
        }
    }
}

﻿using System;
using System.Linq;
using Ziggurat.Contracts;
using Ziggurat.Infrastructure;
using Ziggurat.Infrastructure.EventStore;

namespace Ziggurat.Registration.Domain
{
    public abstract class ApplicationServiceBase<T>
        where T : IAggregateRoot, new()
    {
        private readonly IEventStore _store;

        protected ApplicationServiceBase(IEventStore store)
        {
            if (store == null) throw new ArgumentNullException("store");
            _store = store;
        }

        //this.Update(myId, ar => ar.DoSomething(parameters));
        protected void Update(Guid aggregateId, Action<T> updateAction)
        {
            var events = _store.LoadAll(aggregateId);

            var aggregate = new T();
            aggregate.ApplyFromHistory(events.Events.Select(UnwrapFromEnvelope));

            updateAction(aggregate);

            _store.Append(aggregateId, events.Revision, aggregate.Changes.Select(x=>WrapIntoEnvelope(aggregateId, x)));
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

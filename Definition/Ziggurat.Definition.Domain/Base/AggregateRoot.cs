using System;
using System.Collections.Generic;
using System.Diagnostics;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain
{
    public interface IAggregateRoot
    {
        Guid Id { get; set; }
        int Revision { get; set; }
        void ApplyFromHistory(IEnumerable<IEvent> events);
        IEnumerable<IEvent> Changes { get; }
    }

    public abstract class AggregateRootBase : IAggregateRoot
    {
        public Guid Id { get; set; }
        public int Revision { get; set; }
        private readonly List<IEvent> _changes = new List<IEvent>();
        public IEnumerable<IEvent> Changes { get { return _changes; } }

        public void Apply(IEvent evt)
        {
            MutateState(evt);
            _changes.Add(evt);
        }

        protected virtual void MutateState(IEvent evt)
        {
            ((dynamic)this).When((dynamic)evt);
        }

        public void ApplyFromHistory(IEnumerable<IEvent> events)
        {
            foreach (var evt in events) MutateState(evt);
        }
    }

    public abstract class AggregateRootBase<TState> : AggregateRootBase
        where TState : IState, new()
    {
        protected readonly TState State = new TState();

        protected override void MutateState(IEvent evt)
        {
            State.Mutate(evt);
        }
    }
}

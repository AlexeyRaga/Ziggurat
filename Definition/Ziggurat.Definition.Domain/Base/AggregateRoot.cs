using System.Collections.Generic;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain
{
    public interface IAggregateRoot
    {
        void ApplyFromHistory(IEnumerable<IEvent> events);
        IEnumerable<IEvent> Changes { get; }
    }

    public interface IAggregateState
    {
        void Mutate(IEvent evt);
    }

    public abstract class AggregateRootBase<TState> : IAggregateRoot
        where TState : IAggregateState, new()
    {
        protected readonly TState State = new TState();

        private readonly List<IEvent> _changes = new List<IEvent>();
        public IEnumerable<IEvent> Changes { get { return _changes; } }

        protected void Apply(IEvent evt)
        {
            State.Mutate(evt);
            _changes.Add(evt);
        }

        public void ApplyFromHistory(IEnumerable<IEvent> events)
        {
            foreach (var evt in events) State.Mutate(evt);
        }
    }

    public abstract class AggregateState : IAggregateState
    {
        public void Mutate(IEvent evt)
        {
            ((dynamic)this).When((dynamic)evt);
        }
    }
}

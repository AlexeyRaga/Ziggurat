using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;

namespace Ziggurat.Registration.Domain
{
    public interface IState
    {
        void Mutate(IEvent evt);
    }

    public abstract class State : IState
    {
        public void Mutate(IEvent evt)
        {
            ((dynamic)this).When((dynamic)evt);
        }

        // If there is no "When" catching a specific event, just ignore this event.
        public void When(IEvent evt)
        {
            Debug.WriteLine("{0}: Skipping event '{1}', not interested", this.GetType().Name, evt.GetType().Name);
        }
    }

    /// <summary>
    /// Simply ignores all the events.
    /// </summary>
    /// <remarks>
    /// This state is supposed to be used when an aggregate is not interested in any state
    /// and is just publishing events.
    /// </remarks>
    public class NullState : IState
    {
        public void Mutate(IEvent evt) { }
    }
}

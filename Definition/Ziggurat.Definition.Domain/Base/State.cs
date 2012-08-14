using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Contracts;

namespace Ziggurat.Definition.Domain
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
}

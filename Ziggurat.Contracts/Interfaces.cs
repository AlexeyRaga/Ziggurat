using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Contracts
{
    public interface IMessage { }
    public interface ICommand : IMessage { }
    public interface IEvent : IMessage { }

    public interface IPropertyEvent : IEvent { }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Contracts.Definition
{
    public interface IPropertyDefinitionEvent : IEvent
    {
        Guid PropertyId { get; }
    }
}

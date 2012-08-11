using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Definition.Domain.FormDefinition.Properties
{
    public class PropertyBase
    {
        public ExpandoObject State { get; private set; }
        public PropertyBase(ExpandoObject state)
        {
            State = state;
        }
    }
}

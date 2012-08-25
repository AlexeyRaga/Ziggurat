using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Infrastructure
{
    public static class IdGenerator
    {
        public static Guid GenerateId(Guid nameSpace, string value)
        {
            return GuidGenerator.Create(nameSpace, value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Infrastructure.Serialization
{
    public interface ISerializer
    {
        void Serialize(object value, Stream stream);
        object Deserialize(Type valueType, Stream stream);
    }
}

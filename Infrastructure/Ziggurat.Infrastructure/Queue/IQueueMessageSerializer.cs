using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Infrastructure.Queue
{
    public interface IQueueMessageSerializer
    {
        byte[] Serialize(object message);
        object Deserialize(byte[] message);
    }
}

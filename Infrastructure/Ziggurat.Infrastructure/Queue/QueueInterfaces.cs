using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Infrastructure.Queue
{
    public interface IQueueMessage
    {
        IQueueReader Queue { get; }
        byte[] GetBody();
    }

    public interface IQueueWriter
    {
        void Enqueue(byte[] message);
    }

    public interface IQueueReader
    {
        IQueueMessage Peek();
        void Ack(IQueueMessage msg);
    }

    public interface IQueueFactory
    {
        IQueueWriter CreateWriter(string queueName);
        IQueueReader CreateReader(string queueName);
    }
}

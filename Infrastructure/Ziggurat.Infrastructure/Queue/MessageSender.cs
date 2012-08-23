using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Infrastructure.Queue
{
    public sealed class MessageSender
    {
        private readonly IQueueWriter _writer;
        private readonly IQueueMessageSerializer _serializer;

        public MessageSender(IQueueWriter writer, IQueueMessageSerializer serializer)
        {
            _writer = writer;
            _serializer = serializer;
        }

        public void Send(object message)
        {
            var serializedMessage = _serializer.Serialize(message);
            _writer.Enqueue(serializedMessage);
        }
    }
}

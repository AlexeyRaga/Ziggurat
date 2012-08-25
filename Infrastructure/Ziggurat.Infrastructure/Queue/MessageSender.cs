using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Infrastructure.Serialization;

namespace Ziggurat.Infrastructure.Queue
{
    public sealed class MessageSender
    {
        private readonly IQueueWriter _writer;
        private readonly QueueMessageSerializer _serializer;

        public MessageSender(IQueueWriter writer, ISerializer serializer)
            : this(writer, new QueueMessageSerializer(serializer)) { }

        internal MessageSender(IQueueWriter writer, QueueMessageSerializer serializer)
        {
            if (writer == null) throw new ArgumentNullException("writer");
            if (serializer == null) throw new ArgumentNullException("serializer");

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

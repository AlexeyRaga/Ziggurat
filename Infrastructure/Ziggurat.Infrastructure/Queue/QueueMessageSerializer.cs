using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Infrastructure.Serialization;

namespace Ziggurat.Infrastructure.Queue
{
    internal sealed class QueueMessageSerializer
    {
        private readonly ISerializer _innerSerializer;

        public QueueMessageSerializer(ISerializer serializer)
        {
            if (serializer == null) throw new ArgumentNullException("serializer");
            _innerSerializer = serializer;
        }

        public byte[] Serialize(object message)
        {
            var envelope = new QueueEnvelope(message);
            return _innerSerializer.SerializeToByteArray(envelope);
        }

        public object Deserialize(byte[] message)
        {
            var envelope = _innerSerializer.Deserialize<QueueEnvelope>(message);
            return envelope.Body;
        }

        private sealed class QueueEnvelope
        {
            public object Body { get; set; }
            public QueueEnvelope(object body) { Body = body; }

            public QueueEnvelope() { }
        }
    }
}

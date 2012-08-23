using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Infrastructure.Queue;

namespace Ziggurat.Infrastructure
{
    public sealed class NamespaceBasedQueueRouter
    {
        private readonly IQueueFactory _queueFactory;
        private readonly IQueueMessageSerializer _serializer;

        private readonly ConcurrentDictionary<string, MessageSender> _queues =
            new ConcurrentDictionary<string, MessageSender>();

        public NamespaceBasedQueueRouter(IQueueFactory queueFactory, IQueueMessageSerializer serializer)
        {
            if (queueFactory == null) throw new ArgumentNullException("queueFactory");
            if (serializer == null) throw new ArgumentNullException("serializer");

            _queueFactory = queueFactory;
            _serializer = serializer;
        }

        public void Route(object message)
        {
            if (message == null) return;
            var queueName = GetQueueName(message);

            var sender = _queues.GetOrAdd(queueName, 
                name => new MessageSender(_queueFactory.CreateWriter(name), _serializer));

            sender.Send(message);         
        }

        private string GetQueueName(object message)
        {
            var typeName = message.GetType().FullName;
            return typeName.ToLowerInvariant().Replace('.', '-');
        }
    }
}

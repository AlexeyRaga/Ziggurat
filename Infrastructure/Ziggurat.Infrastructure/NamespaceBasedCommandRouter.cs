using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ziggurat.Infrastructure.Queue;
using Ziggurat.Infrastructure.Serialization;

namespace Ziggurat.Infrastructure
{
    public sealed class NamespaceBasedCommandRouter : ICommandSender
    {
        private readonly IQueueFactory _queueFactory;
        private readonly QueueMessageSerializer _serializer;
        private readonly string _queueNamePrefix;

        private readonly ConcurrentDictionary<string, MessageSender> _queues =
            new ConcurrentDictionary<string, MessageSender>();

        public NamespaceBasedCommandRouter(string queueNamePrefix, IQueueFactory queueFactory, ISerializer serializer)
        {
            if (queueFactory == null) throw new ArgumentNullException("queueFactory");
            if (serializer == null) throw new ArgumentNullException("serializer");

            _queueFactory = queueFactory;
            _serializer = new QueueMessageSerializer(serializer);
            _queueNamePrefix = MakePrefix(queueNamePrefix);
        }

        public void SendCommand(object command)
        {
            if (command == null) return;
            var queueName = GetQueueNameForMessage(command);

            var sender = _queues.GetOrAdd(queueName,
                name => new MessageSender(_queueFactory.CreateWriter(name), _serializer));

            sender.Send(command);      
        }

        private string GetQueueNameForMessage(object message)
        {
            var ns = message.GetType().Namespace.ToLowerInvariant();
            if (ns.Contains(".")) ns = ns.Substring(ns.IndexOf('.') + 1);

            return _queueNamePrefix + ns;
        }

        private string MakePrefix(string prefix)
        {
            if (String.IsNullOrWhiteSpace(prefix)) return String.Empty;
            prefix = prefix.Trim();
            if (prefix.EndsWith("-")) return prefix;
            return prefix + '-';
        }
    }
}

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
    /// <summary>
    ///     Routes commands to queues based on the namespace convention
    /// </summary>
    /// <remarks>
    ///     Generates a queue name based on the message namespace using the following convention:
    ///     First part of the namespace is ignored (as it is always a company/product name)
    ///     Dots are replaced with dashes
    ///     Everything is lowercased.
    ///     <example>
    ///         Namespace <c>Ziggurat.Contracts.Registration</c> will be transformed 
    ///         into <c>contracts-registration</c> and all the messages belong to that namespace will be 
    ///         routed to this queue.
    ///     </example>
    /// </remarks>
    public sealed class NamespaceBasedCommandRouter : ICommandSender
    {
        private readonly IQueueFactory _queueFactory;
        private readonly QueueMessageSerializer _serializer;
        private readonly string _queueNamePrefix;

        //just caching message senders 
        private readonly ConcurrentDictionary<string, MessageSender> _queues =
            new ConcurrentDictionary<string, MessageSender>();

        public NamespaceBasedCommandRouter(string queueNamePrefix, IQueueFactory queueFactory, ISerializer serializer)
        {
            if (queueFactory == null) throw new ArgumentNullException("queueFactory");
            if (serializer == null) throw new ArgumentNullException("serializer");

            //prefix for namespace-based queue names
            _queueNamePrefix = MakePrefix(queueNamePrefix);

            _queueFactory = queueFactory;

            //this guy again :(
            _serializer = new QueueMessageSerializer(serializer);
        }

        public NamespaceBasedCommandRouter(IQueueFactory queueFactory, ISerializer serializer)
            : this(String.Empty, queueFactory, serializer) { }

        public void SendCommand(object command)
        {
            if (command == null) return;

            //get the queue name for the message (by convention)
            var queueName = GetQueueNameForMessage(command);

            //get the sender
            var sender = _queues.GetOrAdd(queueName,
                name => new MessageSender(_queueFactory.CreateWriter(name), _serializer));

            //and finally send
            sender.Send(command);      
        }

        private string GetQueueNameForMessage(object message)
        {
            //rules:
            // 1. Lowercase the namespace
            // 2. Get rid of the firts part (as it is usually the company/product name)
            // 3. Replace dots with dashes
            // 4. Append prefix (if any provided)
            var ns = message.GetType().Namespace.ToLowerInvariant();
            if (ns.Contains(".")) ns = ns.Substring(ns.IndexOf('.') + 1);

            ns = ns.Replace('.', '-');

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

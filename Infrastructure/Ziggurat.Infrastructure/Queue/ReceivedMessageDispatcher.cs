using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ziggurat.Infrastructure.Serialization;

namespace Ziggurat.Infrastructure.Queue
{
    public sealed class ReceivedMessageDispatcher
    {
        private readonly Action<object> _dispatchTo;
        private readonly IIncomingMessagesStream _receiver;
        private readonly QueueMessageSerializer _serializer;

        public ReceivedMessageDispatcher(Action<object> dispatchTo, ISerializer serializer, IIncomingMessagesStream receiver)
        {
            if (dispatchTo == null) throw new ArgumentNullException("dispatchTo");
            if (serializer == null) throw new ArgumentNullException("serializer");
            if (receiver == null) throw new ArgumentNullException("receiver");

            _dispatchTo = dispatchTo;
            _receiver = receiver;
            _serializer = new QueueMessageSerializer(serializer);
        }

        public void Run(CancellationToken cancellation)
        {
            ReceiveMessages(cancellation);
        }

        private void ReceiveMessages(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                IQueueMessage receivedMessage;
                if (_receiver.TryReceive(cancellation, out receivedMessage))
                {
                    HandleMessage(receivedMessage);
                }
                else
                {
                    Thread.Sleep(250);
                }
            }
        }

        private void HandleMessage(IQueueMessage receivedMessage)
        {
            var messageBody = receivedMessage.GetBody();
            var realMessage = _serializer.Deserialize(messageBody);
            _dispatchTo(realMessage);
            receivedMessage.Queue.Ack(receivedMessage);
        }
    }
}

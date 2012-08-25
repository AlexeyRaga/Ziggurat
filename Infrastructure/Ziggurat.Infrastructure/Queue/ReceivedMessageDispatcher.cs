using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ziggurat.Infrastructure.Serialization;

namespace Ziggurat.Infrastructure.Queue
{
    public sealed class ReceivedMessageDispatcher : IDisposable
    {
        private readonly Action<object> _dispatchTo;
        private readonly IMessageReceiver _receiver;
        private readonly QueueMessageSerializer _serializer;

        private readonly CancellationTokenSource _cancellation = new CancellationTokenSource();

        public ReceivedMessageDispatcher(Action<object> dispatchTo, ISerializer serializer, IMessageReceiver receiver)
        {
            if (dispatchTo == null) throw new ArgumentNullException("dispatchTo");
            if (serializer == null) throw new ArgumentNullException("serializer");
            if (receiver == null) throw new ArgumentNullException("receiver");

            _dispatchTo = dispatchTo;
            _receiver = receiver;
            _serializer = new QueueMessageSerializer(serializer);
        }

        public void Run()
        {
            Task.Factory.StartNew(() =>
            {
                ReceiveMessages();

            }, _cancellation.Token);
        }

        private void ReceiveMessages()
        {
            while (!_cancellation.Token.IsCancellationRequested)
            {
                IQueueMessage receivedMessage;
                if (_receiver.TryReceive(_cancellation.Token, out receivedMessage))
                {
                    HandleMessage(receivedMessage);
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

        public void Dispose()
        {
            _cancellation.Dispose();
        }
    }
}

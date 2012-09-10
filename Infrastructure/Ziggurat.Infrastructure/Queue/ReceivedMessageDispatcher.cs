using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ziggurat.Infrastructure.Serialization;

namespace Ziggurat.Infrastructure.Queue
{
    /// <summary>
    /// Receives messages and dispatches them to where it has to.
    /// </summary>
    public sealed class ReceivedMessageDispatcher
    {
        private readonly Action<object> _dispatchTo;
        private readonly IIncomingMessagesStream _receiver;
        private readonly QueueMessageSerializer _serializer;

        /// <summary>
        /// Creates an instance of a dispatcher.
        /// </summary>
        /// <param name="dispatchTo">Where to dispatch incoming messages</param>
        /// <param name="serializer">How to deserialize incoming messages</param>
        /// <param name="receiver">How to receive messages</param>
        public ReceivedMessageDispatcher(
            Action<object> dispatchTo, 
            ISerializer serializer, 
            IIncomingMessagesStream receiver)
        {
            if (dispatchTo == null) throw new ArgumentNullException("dispatchTo");
            if (serializer == null) throw new ArgumentNullException("serializer");
            if (receiver == null) throw new ArgumentNullException("receiver");

            _dispatchTo = dispatchTo;
            _receiver = receiver;

            //did I tell you I hate this guy? Oh, I do...
            _serializer = new QueueMessageSerializer(serializer);
        }

        /// <summary>
        /// Receives and dispatches messages until cancellation is required
        /// </summary>
        /// <param name="cancellation">How to know when to stop</param>
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
                    //Ok, we've got a message. Let's handle it.
                    HandleMessage(receivedMessage);
                }
                else
                {
                    //no mesages? Wait a bit a try again.
                    Thread.Sleep(250);
                }
            }
        }

        private void HandleMessage(IQueueMessage receivedMessage)
        {
            //get the real message
            var messageBody = receivedMessage.GetBody();
            object realMessage;

            //if cannot deserialize the message => Nack it immediately, retries will not help
            try
            {
                realMessage = _serializer.Deserialize(messageBody);
            }
            catch (Exception ex)
            {
                receivedMessage.Queue.Nack(receivedMessage, ex);
                return;
            }

            var tryCount = 0;
            Exception dispatchError = null;

            do
            {
                tryCount++;

                dispatchError = TryDispatchMessage(realMessage);
                if (dispatchError == null)
                {
                    //Important bit: acking the message!
                    receivedMessage.Queue.Ack(receivedMessage);
                    return;
                }

                //something happened, message was not dispatched.
                //wait a bit and try again
                Thread.Sleep(50);

            } while (tryCount < 5);

            // If we are here it means the message has not been dispatched for several times,
            // Nack it and move on
            receivedMessage.Queue.Nack(receivedMessage, dispatchError);
        }

        private Exception TryDispatchMessage(object message)
        {
            try
            {
                _dispatchTo(message);
                return null;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}

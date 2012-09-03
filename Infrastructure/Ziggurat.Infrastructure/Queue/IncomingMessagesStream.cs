using System;
using System.Linq;
using System.Threading;

namespace Ziggurat.Infrastructure.Queue
{
    /// <summary>
    /// Represents an incoming message stream
    /// </summary>
    public interface IIncomingMessagesStream
    {
        /// <summary>
        /// Tries to receive a message.
        /// </summary>
        /// <param name="cancellation">
        /// A cancellation token. 
        /// When cancellation is required this method stops receiving and returns <c>false</c></param>
        /// <param name="message">A received message</param>
        /// <returns><c>true</c> if the message is received, otherwise <c>false</c>.</returns>
        bool TryReceive(CancellationToken cancellation, out IQueueMessage message);
    }

    public sealed class IncomingMessagesStream : IIncomingMessagesStream
    {
        private readonly IQueueReader[] _readers;
        public IncomingMessagesStream(IQueueReader[] readers)
        {
            //check if there are any readers that can be used
            if (readers == null) throw new ArgumentNullException("Readers expected");
            _readers = readers.Where(x => x != null).ToArray();
            if (_readers.Length == 0) throw new ArgumentNullException("Readers expected");
        }

        public bool TryReceive(CancellationToken cancellation, out IQueueMessage message)
        {
            while (!cancellation.IsCancellationRequested)
            {
                //enumerate all the readers, check if there is a message somewhere
                for (var i = 0; i < _readers.Length; i++)
                {
                    var queueMessage = _readers[i].Peek();
                    if (queueMessage == null) continue;

                    //a message found, hurray! return it and report success!
                    message = queueMessage;
                    return true;
                }

                //there were no messages in any readers. Wait for a bit and try again
                var waiter = cancellation.WaitHandle.WaitOne(250);
            }

            message = null;
            return false;
        }
    }
}

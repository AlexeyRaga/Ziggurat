using System;
using System.Linq;
using System.Threading;

namespace Ziggurat.Infrastructure.Queue
{
    public interface IIncomingMessagesStream
    {
        bool TryReceive(CancellationToken cancellation, out IQueueMessage message);
    }

    public sealed class IncomingMessagesStream : IIncomingMessagesStream
    {
        private readonly IQueueReader[] _readers;
        public IncomingMessagesStream(IQueueReader[] readers)
        {
            if (readers == null) throw new ArgumentNullException("Readers expected");
            _readers = readers.Where(x => x != null).ToArray();
            if (_readers.Length == 0) throw new ArgumentNullException("Readers expected");
        }

        public bool TryReceive(CancellationToken cancellation, out IQueueMessage message)
        {
            while (!cancellation.IsCancellationRequested)
            {
                for (var i = 0; i < _readers.Length; i++)
                {
                    var queueMessage = _readers[i].Peek();
                    if (queueMessage == null) continue;

                    message = queueMessage;
                    return true;
                }

                var waiter = cancellation.WaitHandle.WaitOne(100);
            }

            message = null;
            return false;
        }
    }
}

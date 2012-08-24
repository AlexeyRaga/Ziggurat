﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ziggurat.Infrastructure.Queue
{
    public interface IMessageReceiver
    {
        bool TryReceive(CancellationToken cancellation, out IQueueMessage message);
    }

    public sealed class MessageReceiver : IMessageReceiver
    {
        private static IQueueReader[] _readers;
        public MessageReceiver(IQueueReader[] readers)
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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ziggurat.Infrastructure.Queue.FileSystem
{
    internal sealed class FileSystemQueueWriter : IQueueWriter
    {
        private static long CurrentMessageId;

        private readonly string _queueFolder;
        internal FileSystemQueueWriter(string queueFolder)
        {
            _queueFolder = queueFolder;
        }

        public void Enqueue(byte[] message)
        {
            //write into a temp file first (to avoid dirty reads)
            var tmpName = Guid.NewGuid().ToString() + ".tmp";
            var fullTmpName = Path.Combine(_queueFolder, tmpName);
            File.WriteAllBytes(fullTmpName, message);

            //rename to a real file name. Rename is an atomic operation, so it should be all fine.
            var messageId = Interlocked.Increment(ref CurrentMessageId);

            var realFileName = String.Format("{0:yyyy-MM-dd-hh-mm-ss-ffff}-{1:00000000}.msg",
                DateTime.UtcNow,
                messageId);

            var fullRealFileName = Path.Combine(_queueFolder, realFileName);
            File.Move(fullTmpName, fullRealFileName);
        }
    }
}

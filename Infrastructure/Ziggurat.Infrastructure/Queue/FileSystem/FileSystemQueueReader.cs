using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Infrastructure.Queue.FileSystem
{
    internal sealed class FileSystemQueueReader : IQueueReader
    {
        private readonly DirectoryInfo _queueFolder;
        internal FileSystemQueueReader(string queueFolder)
        {
            _queueFolder = new DirectoryInfo(queueFolder);
        }

        public IQueueMessage Peek()
        {
            var firstMessage = _queueFolder.EnumerateFiles("*.msg")
                .FirstOrDefault();

            if (firstMessage == null) return null;

            return new FileSystemQueueMessage(firstMessage, this);
        }

        public void Ack(IQueueMessage msg)
        {
            if (msg == null) throw new ArgumentNullException("msg");
            var fsMessafe = (FileSystemQueueMessage)msg;
            fsMessafe.MessageFile.Delete();
        }
    }
}

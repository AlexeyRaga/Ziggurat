using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Infrastructure.Queue.FileSystem
{
    /// <summary>
    /// Reads the file system queue
    /// </summary>
    internal sealed class FileSystemQueueReader : IQueueReader
    {
        //where is this queue sitting
        private readonly DirectoryInfo _queueFolder;

        internal FileSystemQueueReader(string queueFolder)
        {
            _queueFolder = new DirectoryInfo(queueFolder);
        }

        public IQueueMessage Peek()
        {
            //just get the first message and return it!
            var firstMessage = _queueFolder.EnumerateFiles("*.msg")
                .FirstOrDefault();

            if (firstMessage == null) return null;

            return new FileSystemQueueMessage(firstMessage, this);
        }

        public void Ack(IQueueMessage msg)
        {
            if (msg == null) throw new ArgumentNullException("msg");
            var fsMessafe = (FileSystemQueueMessage)msg;

            //hurray, this message is finally acked! delete it!
            fsMessafe.MessageFile.Delete();
        }

        public void Nack(IQueueMessage msg, Exception exception = null)
        {
            if (msg == null) throw new ArgumentNullException("msg");
            var fsMessafe = (FileSystemQueueMessage)msg;

            var poisonedFolder = EnsurePoisonedQueueFolder();
            var fileNameInPoisonedFolder = Path.Combine(poisonedFolder, fsMessafe.MessageFile.Name);
            fsMessafe.MessageFile.MoveTo(fileNameInPoisonedFolder);
        }

        private string EnsurePoisonedQueueFolder()
        {
            var poisonedFolderName = Path.Combine(_queueFolder.FullName, "poisoned");
            if (!Directory.Exists(poisonedFolderName)) Directory.CreateDirectory(poisonedFolderName);

            return poisonedFolderName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Infrastructure.Queue.FileSystem
{
    internal sealed class FileSystemQueueMessage : IQueueMessage
    {
        public readonly FileInfo MessageFile;
        private readonly FileSystemQueueReader _reader;

        public FileSystemQueueMessage(FileInfo file, FileSystemQueueReader reader)
        {
            MessageFile = file;
            _reader = reader;
        }

        public byte[] GetBody()
        {
            return File.ReadAllBytes(MessageFile.FullName);
        }

        public IQueueReader Queue
        {
            get { return _reader; }
        }
    }
}

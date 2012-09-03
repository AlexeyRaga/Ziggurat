using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziggurat.Infrastructure.Queue.FileSystem
{
    public sealed class FileSystemQueueFactory : IQueueFactory
    {
        private readonly string _queuesFolder;

        /// <summary>
        /// Creates an insance of the file system queue factory
        /// </summary>
        /// <param name="queuesFolder">The location where all the queues will be sitting</param>
        public FileSystemQueueFactory(string queuesFolder)
        {
            if (!Directory.Exists(queuesFolder))
                Directory.CreateDirectory(queuesFolder);

            _queuesFolder = queuesFolder;
        }

        public IQueueWriter CreateWriter(string queueName)
        {
            var queueFolder = BuildQueueFolderAndEnsure(queueName);
            return new FileSystemQueueWriter(queueFolder);
        }

        public IQueueReader CreateReader(string queueName)
        {
            var queueFolder = BuildQueueFolderAndEnsure(queueName);
            return new FileSystemQueueReader(queueFolder);
        }

        private string BuildQueueFolderAndEnsure(string queueName)
        {
            var folder = Path.Combine(_queuesFolder, queueName);
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            return folder;
        }
    }
}

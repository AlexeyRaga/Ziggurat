using System;
using System.Runtime.Serialization;

namespace Ziggurat.Infrastructure.DocumentStore
{
    [Serializable]
    public sealed class DocumentStoreIOException : Exception
    {
        public DocumentStoreIOException() { }

        public DocumentStoreIOException(string message)
            : base(message) { }

        public DocumentStoreIOException(string message, Exception inner)
            : base(message, inner)
        {
        }

        private DocumentStoreIOException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

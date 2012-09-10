using System;
using System.Runtime.Serialization;

namespace Ziggurat.Infrastructure.DocumentStore
{
    [Serializable]
    public sealed class DocumentStoreIOException : Exception
    {
        public object Key
        {
            get { return Data.Contains("docKey") ? Data["docKey"] : null; }
        }

        public DocumentStoreIOException() { }

        public DocumentStoreIOException(string message)
            : base(message) { }

        public DocumentStoreIOException(object documentKey, string message, Exception inner)
            : base(message, inner)
        {
            Data.Add("docKey", documentKey);
        }

        private DocumentStoreIOException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

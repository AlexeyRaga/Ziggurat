using System;
using System.Runtime.Serialization;

namespace Ziggurat.Infrastructure.DocumentStore
{
	[Serializable]
	public class DocumentSerializationException : Exception
	{
		public DocumentSerializationException() { }

		public DocumentSerializationException(string message) 
			: base(message) { }

		public DocumentSerializationException(string message, Exception inner) 
			: base(message, inner) { }

		protected DocumentSerializationException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}

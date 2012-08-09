using System;
using System.Runtime.Serialization;

namespace Ziggurat.Infrastructure.Projections
{
	[Serializable]
	public class ProjectionSerializationException : Exception
	{
		public ProjectionSerializationException() { }

		public ProjectionSerializationException(string message) 
			: base(message) { }

		public ProjectionSerializationException(string message, Exception inner) 
			: base(message, inner) { }

		protected ProjectionSerializationException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}

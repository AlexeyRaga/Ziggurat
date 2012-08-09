using System;
using System.Runtime.Serialization;

namespace Ziggurat.Infrastructure.Projections
{
	[Serializable]
	public class ProjectionIOException : Exception
	{
		public object ProjectionKey
		{
			get { return Data.Contains("projectionKey") ? Data["projectionKey"] : null; }
		}

		public ProjectionIOException() { }

		public ProjectionIOException(string message) 
			: base(message) { }

		public ProjectionIOException(object projectionKey, string message, Exception inner)
			: base(message, inner)
		{
			Data.Add("projectionKey", projectionKey);
		}

		protected ProjectionIOException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}

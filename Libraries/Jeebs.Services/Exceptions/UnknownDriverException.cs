using System;
using System.Collections.Generic;
using System.Text;

namespace Jx.Services
{
	[Serializable]
	public class UnknownDriverException : Exception
	{
		public UnknownDriverException() { }
		public UnknownDriverException(string message) : base(message) { }
		public UnknownDriverException(string message, Exception inner) : base(message, inner) { }
		public UnknownDriverException(Type t) : this(t.FullName) { }
		protected UnknownDriverException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}

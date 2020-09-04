using System;
using System.Collections.Generic;
using System.Text;

namespace Jx.Services.Webhook
{
	[Serializable]
	public class UnknownMessageLevelException : Exception
	{
		public UnknownMessageLevelException() { }
		public UnknownMessageLevelException(string message) : base(message) { }
		public UnknownMessageLevelException(string message, Exception inner) : base(message, inner) { }
		protected UnknownMessageLevelException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}

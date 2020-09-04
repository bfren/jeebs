using System;
using System.Collections.Generic;
using System.Text;

namespace Jx.Services.Webhook
{
	/// <summary>
	/// Unknown MessageLevel
	/// </summary>
	[Serializable]
	public class UnknownMessageLevelException : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public UnknownMessageLevelException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		public UnknownMessageLevelException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public UnknownMessageLevelException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected UnknownMessageLevelException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}

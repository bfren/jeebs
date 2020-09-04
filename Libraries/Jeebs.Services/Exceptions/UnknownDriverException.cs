using System;
using System.Collections.Generic;
using System.Text;

namespace Jx.Services
{
	/// <summary>
	/// Unknown Driver
	/// </summary>
	[Serializable]
	public class UnknownDriverException : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public UnknownDriverException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		public UnknownDriverException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public UnknownDriverException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="t"></param>
		public UnknownDriverException(Type t) : this(t.FullName) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected UnknownDriverException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}

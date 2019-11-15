using System;
using System.Collections.Generic;
using System.Text;

namespace Jx.Data
{
	/// <summary>
	/// Database Connection Exception
	/// </summary>
	public class ConnectionException : Exception
	{
		/// <summary>
		/// Empty constructor
		/// </summary>
		public ConnectionException() { }

		/// <summary>
		/// Construct exception
		/// </summary>
		/// <param name="message">Message</param>
		public ConnectionException(string message) : base(message) { }

		/// <summary>
		/// Construct exception
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Inner exception</param>
		public ConnectionException(string message, Exception inner) : base(message, inner) { }
	}
}

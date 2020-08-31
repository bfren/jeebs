using System;
using System.Collections.Generic;
using System.Text;

namespace Jx.Config
{
	/// <summary>
	/// No DB Connections
	/// </summary>
	[Serializable]
	public class NoDbConnectionsException : Exception
	{
		/// <summary>
		/// Create exception
		/// </summary>
		public NoDbConnectionsException() { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		public NoDbConnectionsException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message"></param>
		/// <param name="inner"></param>
		public NoDbConnectionsException(string message, Exception inner) : base(message, inner) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="info"></param>
		/// <param name="context"></param>
		protected NoDbConnectionsException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}

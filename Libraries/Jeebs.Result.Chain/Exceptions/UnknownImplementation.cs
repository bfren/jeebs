using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;

namespace Jx.Result
{
	/// <summary>
	/// Thrown when a custom implementation of <see cref="IR"/> is used.
	/// </summary>
	public class UnknownImplementationException : Exception
	{
		private readonly static string error = $"Unknown implementation of {typeof(IR)}.";

		/// <summary>
		/// Create exception
		/// </summary>
		public UnknownImplementationException() : base(error) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message">Message</param>
		public UnknownImplementationException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Inner Exception</param>
		public UnknownImplementationException(string message, Exception inner) : base(message, inner) { }
	}
}

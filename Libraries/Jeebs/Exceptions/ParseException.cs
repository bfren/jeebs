using System;

namespace Jx
{
	/// <summary>
	/// ParseException
	/// </summary>
	public sealed class ParseException : Exception
	{
		/// <summary>
		/// Empty constructor
		/// </summary>
		public ParseException() { }

		/// <summary>
		/// Construct exception
		/// </summary>
		/// <param name="message">Message</param>
		public ParseException(in string message) : base(message) { }

		/// <summary>
		/// Construct exception
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Inner exception</param>
		public ParseException(in string message, in Exception inner) : base(message, inner) { }
	}
}

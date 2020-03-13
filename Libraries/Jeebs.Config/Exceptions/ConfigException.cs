using System;
using System.Collections.Generic;
using System.Text;

namespace Jx
{
	/// <summary>
	/// ParseException
	/// </summary>
	public sealed class ConfigException : Exception
	{
		/// <summary>
		/// Empty constructor
		/// </summary>
		public ConfigException() { }

		/// <summary>
		/// Construct exception
		/// </summary>
		/// <param name="message">Message</param>
		public ConfigException(string message) : base(message) { }

		/// <summary>
		/// Construct exception
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Inner exception</param>
		public ConfigException(string message, Exception inner) : base(message, inner) { }
	}
}

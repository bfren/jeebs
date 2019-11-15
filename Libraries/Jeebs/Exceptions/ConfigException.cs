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
		public ConfigException(in string message) : base(message) { }

		/// <summary>
		/// Construct exception
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Inner exception</param>
		public ConfigException(in string message, in Exception inner) : base(message, inner) { }
	}
}

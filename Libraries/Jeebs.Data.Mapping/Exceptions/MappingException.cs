using System;
using System.Collections.Generic;
using System.Text;

namespace Jx.Data
{
	/// <summary>
	/// Database Mapping Exception
	/// </summary>
	public class MappingException : Exception
	{
		/// <summary>
		/// Empty constructor
		/// </summary>
		public MappingException() { }

		/// <summary>
		/// Construct exception
		/// </summary>
		/// <param name="message">Message</param>
		public MappingException(string message) : base(message) { }

		/// <summary>
		/// Construct exception
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Inner exception</param>
		public MappingException(string message, Exception inner) : base(message, inner) { }
	}
}

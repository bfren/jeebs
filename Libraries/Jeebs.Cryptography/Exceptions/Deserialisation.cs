using System;
using System.Collections.Generic;
using System.Text;

namespace Jx.Cryptography
{
	/// <summary>
	/// Decryption Exception
	/// </summary>
	public class DeserialisationException : Exception
	{
		/// <summary>
		/// Empty constructor
		/// </summary>
		public DeserialisationException() { }

		/// <summary>
		/// Construct object
		/// </summary>
		/// <param name="message">Message</param>
		public DeserialisationException(string message) : base(message) { }

		/// <summary>
		/// Construct object
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Inner exception</param>
		public DeserialisationException(string message, Exception inner) : base(message, inner) { }
	}
}

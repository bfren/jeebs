using System;
using System.Collections.Generic;
using System.Text;

namespace Jx.Cryptography
{
	/// <summary>
	/// Decryption Exception
	/// </summary>
	public class DecryptionException : Exception
	{
		/// <summary>
		/// Empty constructor
		/// </summary>
		public DecryptionException() { }

		/// <summary>
		/// Construct object
		/// </summary>
		/// <param name="message">Message</param>
		public DecryptionException(string message) : base(message) { }

		/// <summary>
		/// Construct object
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Inner exception</param>
		public DecryptionException(string message, Exception inner) : base(message, inner) { }
	}
}

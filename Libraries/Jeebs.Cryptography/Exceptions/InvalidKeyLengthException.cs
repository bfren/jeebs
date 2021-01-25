using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Cryptography;

namespace Jx.Cryptography
{
	/// <summary>
	/// See <see cref="Lockable{T}.Lock(byte[])"/>
	/// </summary>
	public class InvalidKeyLengthException : Exception
	{
		/// <summary>
		/// Exception message format
		/// </summary>
		public const string Format = "Key length must be {0} bytes.";

		/// <summary>
		/// Create exception
		/// </summary>
		public InvalidKeyLengthException() : this(string.Format(Format, Lockable.KeyLength)) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message">Message</param>
		public InvalidKeyLengthException(string message) : base(message) { }

		/// <summary>
		/// Create exception
		/// </summary>
		/// <param name="message">Message</param>
		/// <param name="inner">Exception</param>
		public InvalidKeyLengthException(string message, Exception inner) : base(message, inner) { }
	}
}

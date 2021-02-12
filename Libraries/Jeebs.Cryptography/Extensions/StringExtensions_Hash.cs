using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sodium;

namespace Jeebs.Cryptography
{
	/// <summary>
	/// String Extensions: Hash
	/// </summary>
	public static class StringExtensions_Hash
	{
		/// <summary>
		/// Compute a hash
		/// A 32 byte hash returns a string of length 44
		/// A 64 byte hash (default) returns a string of length 88
		/// </summary>
		/// <param name="this">String to hash</param>
		/// <param name="length">Hash length (in bytes)</param>
		/// <returns>Base 64 encoded string</returns>
		public static string Hash(this string @this, int length = 64)
		{
			if (string.IsNullOrEmpty(@this))
			{
				return string.Empty;
			}

			var hash = GenericHash.Hash(Encoding.UTF8.GetBytes(@this), null, length);
			return Convert.ToBase64String(hash);
		}
	}
}

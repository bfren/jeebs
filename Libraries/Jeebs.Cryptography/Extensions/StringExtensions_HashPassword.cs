using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sodium;

namespace Jeebs.Cryptography
{
	/// <summary>
	/// String Extensions: HashPassword
	/// </summary>
	public static class StringExtensions_HashPassword
	{
		/// <summary>
		/// Hash a password using argon2id - returns hash containing the salt (of length 128 bytes)
		/// </summary>
		/// <param name="this">Password to hash</param>
		/// <returns>Password hash</returns>
		public static string HashPassword(this string @this)
		{
			if (string.IsNullOrEmpty(@this))
			{
				throw new ArgumentNullException(nameof(@this));
			}

			return PasswordHash.ArgonHashString(@this, PasswordHash.StrengthArgon.Moderate);
		}
	}
}

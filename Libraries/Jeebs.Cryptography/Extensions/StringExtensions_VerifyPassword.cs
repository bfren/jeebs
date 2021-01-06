using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sodium;

namespace Jeebs.Cryptography
{
	/// <summary>
	/// String Extensions: VerifyPassword
	/// </summary>
	public static class StringExtensions_VerifyPassword
	{
		/// <summary>
		/// Verify a password hashed using argon2id
		/// </summary>
		/// <param name="this">Password hash</param>
		/// <param name="password">Password to verify</param>
		/// <returns>Whether or not the password is valid</returns>
		public static bool VerifyPassword(this string @this, string password)
		{
			if (string.IsNullOrEmpty(@this))
			{
				return false;
			}

			if (string.IsNullOrEmpty(password))
			{
				return false;
			}

			return PasswordHash.ArgonHashStringVerify(@this, password);
		}
	}
}

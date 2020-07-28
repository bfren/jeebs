using System;
using System.IO;
using System.Text;
using Sodium;

namespace Jeebs.Cryptography
{
	/// <summary>
	/// Encryption Extensions
	/// </summary>
	public static partial class EncryptionExtensions
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
				throw new ArgumentNullException(nameof(@this));
			}

			var hash = GenericHash.Hash(Encoding.UTF8.GetBytes(@this), null, length);
			return Convert.ToBase64String(hash);
		}

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
				throw new ArgumentNullException(nameof(@this));
			}

			if (string.IsNullOrEmpty(password))
			{
				throw new ArgumentNullException(nameof(password));
			}

			return PasswordHash.ArgonHashStringVerify(@this, password);
		}
	}
}

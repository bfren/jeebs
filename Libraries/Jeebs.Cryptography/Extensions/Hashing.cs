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
		/// <param name="input">String to hash</param>
		/// <param name="length">Hash length (in bytes)</param>
		/// <returns>Base 64 encoded string</returns>
		public static string Hash(this string input, int length = 64)
		{
			if (string.IsNullOrEmpty(input))
			{
				throw new ArgumentNullException(nameof(input));
			}

			var hash = GenericHash.Hash(Encoding.UTF8.GetBytes(input), null, length);
			return Convert.ToBase64String(hash);
		}

		/// <summary>
		/// Hash a password using argon2id - returns hash containing the salt (of length 128 bytes)
		/// </summary>
		/// <param name="password">Password to hash</param>
		/// <returns>Password hash</returns>
		public static string HashPassword(this string password)
		{
			if (string.IsNullOrEmpty(password))
			{
				throw new ArgumentNullException(nameof(password));
			}

			return PasswordHash.ArgonHashString(password, PasswordHash.StrengthArgon.Moderate);
		}

		/// <summary>
		/// Verify a password hashed using argon2id
		/// </summary>
		/// <param name="hash">Password hash</param>
		/// <param name="password">Password to verify</param>
		/// <returns>Whether or not the password is valid</returns>
		public static bool VerifyPassword(this string hash, string password)
		{
			if (string.IsNullOrEmpty(hash))
			{
				throw new ArgumentNullException(nameof(hash));
			}

			if (string.IsNullOrEmpty(password))
			{
				throw new ArgumentNullException(nameof(password));
			}

			return PasswordHash.ArgonHashStringVerify(hash, password);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Util;

namespace Jeebs.Cryptography
{
	/// <summary>
	/// Decryption Extensions
	/// </summary>
	public static class StringExtensions_Decrypt
	{
		/// <summary>
		/// Decrypt a string using the specified key
		/// </summary>
		/// <typeparam name="T">Type of object being encrypted</typeparam>
		/// <param name="this">JSON-serialised Box</param>
		/// <param name="key">Encryption Key (must be 32 bytes)</param>
		public static Option<T> Decrypt<T>(this string @this, byte[] key)
			=> from l in Json.Deserialise<Locked<T>>(@this)
			   from c in l.Unlock(key)
			   select c.Contents;

		/// <summary>
		/// Decrypt a string using the specified key
		/// </summary>
		/// <typeparam name="T">Type of object being encrypted</typeparam>
		/// <param name="this">JSON-serialised Box</param>
		/// <param name="key">Encryption Key</param>
		public static Option<T> Decrypt<T>(this string @this, string key)
			=> from l in Json.Deserialise<Locked<T>>(@this)
			   from c in l.Unlock(key)
			   select c.Contents;

		/// <summary>
		/// Decrypt a string using the specified key
		/// </summary>
		/// <param name="this">JSON-serialised Box</param>
		/// <param name="key">Encryption Key (must be 32 bytes)</param>
		public static string Decrypt(this string @this, byte[] key)
			=> Decrypt<string>(@this, key).Unwrap(() => string.Empty);

		/// <summary>
		/// Decrypt a string using the specified key
		/// </summary>
		/// <param name="this">JSON-serialised Box</param>
		/// <param name="key">Encryption Key</param>
		public static string Decrypt(this string @this, string key)
			=> Decrypt<string>(@this, key).Unwrap(() => string.Empty);
	}
}

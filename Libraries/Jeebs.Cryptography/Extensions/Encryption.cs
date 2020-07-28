using Jeebs.Util;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Jeebs.Cryptography
{
	/// <summary>
	/// Encryption Extensions
	/// </summary>
	public static partial class EncryptionExtensions
	{
		/// <summary>
		/// Encrypt an object using the specified key
		/// </summary>
		/// <typeparam name="T">Type of object being encrypted</typeparam>
		/// <param name="this">String to encrypt</param>
		/// <param name="key">Encryption Key (must be 32 bytes)</param>
		/// <returns>JSON-serialised Box</returns>
		public static string Encrypt<T>(this T @this, byte[] key)
			=> Json.Serialise(LockedBox<T>.Create(@this, key));

		/// <summary>
		/// Encrypt an object using the specified key
		/// </summary>
		/// <typeparam name="T">Type of object being encrypted</typeparam>
		/// <param name="this">String to encrypt</param>
		/// <param name="key">Encryption key</param>
		/// <returns>JSON-serialised Box</returns>
		public static string Encrypt<T>(this T @this, string key)
			=> Json.Serialise(LockedBox<T>.Create(@this, key));

		/// <summary>
		/// Decrypt a string using the specified key
		/// </summary>
		/// <typeparam name="T">Type of object being encrypted</typeparam>
		/// <param name="this">JSON-serialised Box</param>
		/// <param name="key">Encryption Key (must be 32 bytes)</param>
		/// <returns>Decrypted object</returns>
		public static T Decrypt<T>(this string @this, byte[] key)
			=> LockedBox<T>.Open(@this, key).Value;

		/// <summary>
		/// Decrypt a string using the specified key
		/// </summary>
		/// <typeparam name="T">Type of object being encrypted</typeparam>
		/// <param name="this">JSON-serialised Box</param>
		/// <param name="key">Encryption Key</param>
		/// <returns>Decrypted object</returns>
		public static T Decrypt<T>(this string @this, string key)
			=> LockedBox<T>.Open(@this, key).Value;

		/// <summary>
		/// Decrypt a string using the specified key
		/// </summary>
		/// <param name="this">JSON-serialised Box</param>
		/// <param name="key">Encryption Key (must be 32 bytes)</param>
		/// <returns>Decrypted string</returns>
		public static string Decrypt(this string @this, byte[] key)
			=> Decrypt<string>(@this, key);

		/// <summary>
		/// Decrypt a string using the specified key
		/// </summary>
		/// <param name="this">JSON-serialised Box</param>
		/// <param name="key">Encryption Key</param>
		/// <returns>Decrypted string</returns>
		public static string Decrypt(this string @this, string key)
			=> Decrypt<string>(@this, key);
	}
}

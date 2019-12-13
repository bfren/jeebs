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
		/// <param name="input">String to encrypt</param>
		/// <param name="key">Encryption Key (must be 32 bytes)</param>
		/// <returns>JSON-serialised Box</returns>
		public static string Encrypt<T>(this T input, byte[] key) => Json.Serialise(LockedBox<T>.Create(input, key));

		/// <summary>
		/// Encrypt an object using the specified key
		/// </summary>
		/// <typeparam name="T">Type of object being encrypted</typeparam>
		/// <param name="input">String to encrypt</param>
		/// <param name="key">Encryption key</param>
		/// <returns>JSON-serialised Box</returns>
		public static string Encrypt<T>(this T input, string key) => Json.Serialise(LockedBox<T>.Create(input, key));

		/// <summary>
		/// Decrypt a string using the specified key
		/// </summary>
		/// <typeparam name="T">Type of object being encrypted</typeparam>
		/// <param name="json">JSON-serialised Box</param>
		/// <param name="key">Encryption Key (must be 32 bytes)</param>
		/// <returns>Decrypted object</returns>
		public static T Decrypt<T>(this string json, byte[] key) => LockedBox<T>.Open(json, key).Value;

		/// <summary>
		/// Decrypt a string using the specified key
		/// </summary>
		/// <typeparam name="T">Type of object being encrypted</typeparam>
		/// <param name="json">JSON-serialised Box</param>
		/// <param name="key">Encryption Key</param>
		/// <returns>Decrypted object</returns>
		public static T Decrypt<T>(this string json, string key) => LockedBox<T>.Open(json, key).Value;

		/// <summary>
		/// Decrypt a string using the specified key
		/// </summary>
		/// <param name="json">JSON-serialised Box</param>
		/// <param name="key">Encryption Key (must be 32 bytes)</param>
		/// <returns>Decrypted string</returns>
		public static string Decrypt(this string json, byte[] key) => Decrypt<string>(json, key);

		/// <summary>
		/// Decrypt a string using the specified key
		/// </summary>
		/// <param name="json">JSON-serialised Box</param>
		/// <param name="key">Encryption Key</param>
		/// <returns>Decrypted string</returns>
		public static string Decrypt(this string json, string key) => Decrypt<string>(json, key);
	}
}

// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;

namespace Jeebs.Cryptography;

public static partial class StringExtensions
{
	/// <summary>
	/// Decrypt a string using the specified key.
	/// </summary>
	/// <typeparam name="T">Type of object being encrypted.</typeparam>
	/// <param name="this">JSON-serialised Box.</param>
	/// <param name="key">Encryption Key (must be 32 bytes).</param>
	/// <returns>Decrypted object.</returns>
	public static Result<T> Decrypt<T>(this string @this, byte[] key) =>
		from l in JsonF.Deserialise<Locked<T>>(@this)
		from c in l.Unlock(key)
		select c.Contents;

	/// <summary>
	/// Decrypt a string using the specified key.
	/// </summary>
	/// <typeparam name="T">Type of object being encrypted.</typeparam>
	/// <param name="this">JSON-serialised Box.</param>
	/// <param name="key">Encryption Key (must be 32 bytes).</param>
	/// <returns>Decrypted object.</returns>
	public static Result<T> Decrypt<T>(this string @this, string key) =>
		from l in JsonF.Deserialise<Locked<T>>(@this)
		from c in l.Unlock(key)
		select c.Contents;

	/// <summary>
	/// Decrypt a string using the specified key.
	/// </summary>
	/// <param name="this">JSON-serialised Box.</param>
	/// <param name="key">Encryption Key (must be 32 bytes).</param>
	/// <returns>Decrypted object.</returns>
	public static string Decrypt(this string @this, byte[] key) =>
		Decrypt<string>(@this, key).Discard().Unwrap(() => string.Empty);

	/// <summary>
	/// Decrypt a string using the specified key.
	/// </summary>
	/// <param name="this">JSON-serialised Box.</param>
	/// <param name="key">Encryption Key (must be 32 bytes).</param>
	/// <returns>Decrypted object.</returns>
	public static string Decrypt(this string @this, string key) =>
		Decrypt<string>(@this, key).Discard().Unwrap(() => string.Empty);
}

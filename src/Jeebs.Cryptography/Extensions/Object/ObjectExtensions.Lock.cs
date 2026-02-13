// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Cryptography.Functions;

namespace Jeebs.Cryptography;

public static partial class ObjectExtensions
{
	/// <summary>
	/// Create a Locked box by encrypting <paramref name="this"/> using the specified key and return it serialised as JSON.
	/// </summary>
	/// <typeparam name="T">Type of object being encrypted.</typeparam>
	/// <param name="this">Value to encrypt.</param>
	/// <param name="key">Encryption Key (must be 32 bytes).</param>
	/// <returns>Locked box.</returns>
	public static Result<Locked<T>> Lock<T>(this T @this, byte[] key) =>
		CryptoF.Lock(@this, key);

	/// <summary>
	/// Create a Locked box by encrypting <paramref name="this"/> using the specified key and return it serialised as JSON.
	/// </summary>
	/// <typeparam name="T">Type of object being encrypted.</typeparam>
	/// <param name="this">Value to encrypt.</param>
	/// <param name="key">Encryption Key.</param>
	/// <returns>Locked box.</returns>
	public static Result<Locked<T>> Lock<T>(this T @this, string key) =>
		CryptoF.Lock(@this, key);
}

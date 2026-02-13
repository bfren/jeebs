// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Cryptography.Functions;

namespace Jeebs.Cryptography;

/// <summary>
/// Contains contents that can been encrypted.
/// </summary>
/// <typeparam name="T">Value type.</typeparam>
public sealed record class Lockable<T>(T Value) : Lockable, IMonad<Lockable<T>, T>
{
	/// <summary>
	/// Lock this object.
	/// </summary>
	/// <param name="key">Encryption key - must be <see cref="Lockable.KeyLength"/> bytes.</param>
	/// <returns>Locked box.</returns>
	public Result<Locked<T>> Lock(byte[] key) =>
		CryptoF.Lock(Value, key);

	/// <summary>
	/// Lock this object.
	/// </summary>
	/// <param name="key">Encryption key.</param>
	/// <returns>Locked box.</returns>
	public Result<Locked<T>> Lock(string key) =>
		CryptoF.Lock(Value, key);
}

/// <summary>
/// Holds constants for <see cref="Lockable{T}"/>.
/// </summary>
public abstract record class Lockable
{
	/// <summary>
	/// Length of encryption key (if it's a byte array).
	/// </summary>
	public static readonly int KeyLength = 32;
}

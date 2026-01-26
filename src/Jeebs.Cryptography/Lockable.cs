// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Cryptography.Functions;

namespace Jeebs.Cryptography;

/// <summary>
/// Contains contents that can been encrypted.
/// </summary>
/// <typeparam name="T">Value type.</typeparam>
/// <param name="contents">Contents.</param>
public sealed class Lockable<T>(T contents) : Lockable
{
	/// <summary>
	/// Lockable contents.
	/// </summary>
	public T Contents { get; private init; } =
		contents;

	/// <summary>
	/// Lock this object.
	/// </summary>
	/// <param name="key">Encryption key - must be <see cref="Lockable.KeyLength"/> bytes.</param>
	/// <returns>Locked box.</returns>
	public Result<Locked<T>> Lock(byte[] key) =>
		CryptoF.Lock(Contents, key);

	/// <summary>
	/// Lock this object.
	/// </summary>
	/// <param name="key">Encryption key.</param>
	/// <returns>Locked box.</returns>
	public Result<Locked<T>> Lock(string key) =>
		CryptoF.Lock(Contents, key);
}

/// <summary>
/// Holds constants for <see cref="Lockable{T}"/>.
/// </summary>
public abstract class Lockable
{
	/// <summary>
	/// Length of encryption key (if it's a byte array).
	/// </summary>
	public static readonly int KeyLength = 32;
}

// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Cryptography.Functions;

public static partial class CryptoF
{
	/// <summary>
	/// Create a locked box to secure <paramref name="contents"/> using <paramref name="key"/>
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	/// <param name="contents">Value to encrypt</param>
	/// <param name="key">Encryption key</param>
	public static Maybe<Locked<T>> Lock<T>(T contents, byte[] key) =>
		new Lockable<T>(contents).Lock(key);

	/// <inheritdoc cref="Lock{T}(T, byte[])"/>
	public static Locked<T> Lock<T>(T contents, string key) =>
		new Lockable<T>(contents).Lock(key);
}

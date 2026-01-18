// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Cryptography.Functions;

public static partial class CryptoF
{$1/// <summary>
$2/// $3$4.
$5/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	/// <param name="contents">Value to encrypt.</param>
	/// <param name="key">Encryption key.</param>
	/// <returns>Locked box.</returns>
	public static Result<Locked<T>> Lock<T>(T contents, byte[] key) =>
		contents switch
		{
			T x =>
				new Lockable<T>(x).Lock(key),

			_ =>
				new Locked<T>()
		};

	/// <inheritdoc cref="Lock{T}(T, byte[])"/>
	public static Result<Locked<T>> Lock<T>(T contents, string key) =>
		contents switch
		{
			T x =>
				new Lockable<T>(x).Lock(key),

			_ =>
				new Locked<T>()
		};
}

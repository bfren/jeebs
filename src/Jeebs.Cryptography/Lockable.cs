// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Cryptography;

/// <summary>
/// Contains contents that can been encrypted.
/// </summary>
/// <typeparam name="T">Value type.</typeparam>
/// <param name="contents">Contents.</param>
public sealed class Lockable<T>(T contents) : Lockable
{$1/// <summary>
$2/// $3$4.
$5/// </summary>
	public T Contents { get; private init; } =
		contents;$1/// <summary>
$2/// $3$4.
$5/// </summary>
	/// <param name="key">Encryption key - must be <see cref="Lockable.KeyLength"/> bytes.</param>
	/// <returns>Locked box.</returns>
	public Result<Locked<T>> Lock(byte[] key) =>
		key.Length switch
		{
			int l when l == KeyLength =>
				R.Try(
					() => new Locked<T>(Contents, key),
					e => R.Fail(nameof(Lockable<>), nameof(Lock), e)
				),

			_ =>
				R.Fail(nameof(Lockable<>), nameof(Lock), "Key must be {Bytes} bytes long.", KeyLength)
		};$1/// <summary>
$2/// $3$4.
$5/// </summary>
	/// <param name="key">Encryption key.</param>
	/// <returns>Locked object.</returns>
	public Result<Locked<T>> Lock(string key) =>
		R.Try(
			() => new Locked<T>(Contents, key),
			e => R.Fail(nameof(Lockable<>), nameof(Lock), e)
		);
}

/// <summary>
/// Holds constants for <see cref="Lockable{T}"/>.
/// </summary>
public abstract class Lockable()
{$1/// <summary>
$2/// $3$4.
$5/// </summary>
	public static readonly int KeyLength = 32;
}

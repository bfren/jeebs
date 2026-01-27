// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;

namespace Jeebs.Cryptography.Functions;

public static partial class CryptoF
{
	/// <summary>
	/// Validates the specified contents and returns a successful result if the value is not null; otherwise, returns a
	/// failed result indicating that the contents are required.
	/// </summary>
	/// <typeparam name="T">The type of the contents to validate.</typeparam>
	/// <param name="contents">The value to validate. Cannot be null.</param>
	/// <returns>A <see cref="Result{T}"/> containing the validated contents if not null; otherwise, a failed result with an error
	/// message.</returns>
	internal static Result<string> CheckContents<T>(T contents) =>
		contents switch
		{
			// string values do not need serialisation
			string str =>
				str,

			// serialise objects as JSON
			T obj =>
				JsonF.Serialise(obj),

			// contents cannot be null
			_ =>
				R.Fail("Contents cannot be null.").Ctx(nameof(CryptoF), nameof(Lock))
		};

	/// <summary>
	/// Validates that the specified key has the required length for cryptographic operations.
	/// </summary>
	/// <remarks>Use this method to ensure that a key meets the length requirements before performing operations
	/// that depend on key size. The method does not modify the input key.</remarks>
	/// <param name="key">The key to validate. Must be a non-null byte array with a length equal to <see cref="Lockable.KeyLength"/>.</param>
	/// <returns>A <see cref="Result{T}"/> containing the original key if the length is valid; otherwise, a failure result with an
	/// error message.</returns>
	internal static Result<byte[]> CheckKey(byte[] key) =>
		(key.Length == Lockable.KeyLength) switch
		{
			true =>
				key,

			false =>
				R.Fail("Key must be {Bytes} bytes long.", Lockable.KeyLength)
					.Ctx(nameof(CryptoF), nameof(Lock))
		};

	/// <summary>
	/// Create a locked box to secure <paramref name="contents"/> using <paramref name="key"/>.
	/// </summary>
	/// <typeparam name="T">Value type.</typeparam>
	/// <param name="contents">Value to encrypt.</param>
	/// <param name="key">Encryption key.</param>
	/// <returns>Locked box.</returns>
	public static Result<Locked<T>> Lock<T>(T contents, byte[] key) =>
		from c in CheckContents(contents)
		from k in CheckKey(key)
		select new Locked<T>(c, k);

	/// <inheritdoc cref="Lock{T}(T, byte[])"/>
	public static Result<Locked<T>> Lock<T>(T contents, string key) =>
		from c in CheckContents(contents)
		select new Locked<T>(c, key);
}

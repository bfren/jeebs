// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Security.Cryptography;
using System.Text;
using Jeebs.Functions;
using Sodium;
using Sodium.Exceptions;

namespace Jeebs.Cryptography;

/// <summary>
/// Contains contents that have been encrypted - see <see cref="Locked{T}.EncryptedContents"/>.
/// </summary>
/// <typeparam name="T">Contents type.</typeparam>
public sealed class Locked<T>
{$1/// <summary>
$2/// $3$4.
$5/// </summary>
	public Maybe<byte[]> EncryptedContents { get; init; } =
		M.None;$1/// <summary>
$2/// $3$4.
$5/// </summary>
	public byte[] Salt { get; init; }$1/// <summary>
$2/// $3$4.
$5/// </summary>
	public byte[] Nonce { get; init; }$1/// <summary>
$2/// $3$4.
$5/// </summary>
	public Locked() =>
		(Salt, Nonce) = (SodiumCore.GetRandomBytes(16), SecretBox.GenerateNonce());

	internal Locked(T contents, byte[] key) : this() =>
		EncryptedContents = JsonF.Serialise(contents)
			.Discard()
			.Map(x => SecretBox.Create(x, Nonce, key));

	internal Locked(T contents, string key) : this() =>
		EncryptedContents = JsonF.Serialise(contents)
			.Discard()
			.Map(x => SecretBox.Create(x, Nonce, HashKey(key)));$1/// <summary>
$2/// $3$4.
$5/// </summary>
	/// <param name="key">Encryption Key.</param>
	/// <result>Unlocked box.</result>
	public Result<Lockable<T>> Unlock(byte[] key)
	{
		static Result<Lockable<T>> fail(string message, params object?[] args) =>
			R.Fail(nameof(Locked<>), nameof(Unlock), message, args);

		return EncryptedContents.Match(
			none: () => fail("There are no encrypted contents to unlock."),
			some: x =>
			{
				try
				{
					// Open encrypted contents
					var secret = SecretBox.Open(x, Nonce, key);

					// Deserialise contents and return
					return JsonF.Deserialise<T>(Encoding.UTF8.GetString(secret))
						.Map(x => new Lockable<T>(x));
				}
				catch (KeyOutOfRangeException ex)
				{
					return fail("Invalid key: {Exception}.", ex);
				}
				catch (NonceOutOfRangeException ex)
				{
					return fail("Invalid nonce: {Exception}.", ex);
				}
				catch (CryptographicException ex)
				{
					return fail("Incorrect key or nonce: {Exception}.", ex);
				}
				catch (Exception ex)
				{
					return R.Fail(nameof(Locked<>), nameof(Unlock), ex);
				}
			}
		);
	}$1/// <summary>
$2/// $3$4.
$5/// </summary>
	/// <param name="key">Encryption Key.</param>
	/// <returns>Unlocked box.</returns>
	public Result<Lockable<T>> Unlock(string key) =>
		Unlock(HashKey(key));$1/// <summary>
$2/// $3$4.
$5/// </summary>
	/// <returns>JSON.</returns>
	public Result<string> Serialise() =>
		EncryptedContents.Match(
			none: () => JsonF.Empty,
			some: x => JsonF.Serialise(this)
		);

	private byte[] HashKey(string key) =>
		GenericHash.Hash(key, Salt, Lockable.KeyLength);
}

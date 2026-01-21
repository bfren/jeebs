// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Cryptography.Functions;
using Jeebs.Functions;
using Sodium;

namespace Jeebs.Cryptography;

/// <summary>
/// Contains contents that have been encrypted - see <see cref="EncryptedContents"/>.
/// </summary>
public abstract class Locked
{
	/// <summary>
	/// Encrypted contents.
	/// </summary>
	public Maybe<byte[]> EncryptedContents { get; init; } =
		M.None;

	/// <summary>
	/// Encryption salt.
	/// </summary>
	public byte[] Salt { get; init; }

	/// <summary>
	/// Encryption nonce.
	/// </summary>
	public byte[] Nonce { get; init; }

	/// <summary>
	/// Create new Locked box with random salt and nonce.
	/// </summary>
	internal Locked() =>
		(Salt, Nonce) = (SodiumCore.GetRandomBytes(16), SecretBox.GenerateNonce());
}

/// <inheritdoc/>
/// <typeparam name="T">Contents type.</typeparam>
public sealed class Locked<T> : Locked
{
	/// <summary>
	/// Required for JSON deserialisation.
	/// </summary>
	public Locked() { }

	/// <summary>
	/// Internal creation only.
	/// </summary>
	/// <param name="json">JSON-encoded contents to encrypt.</param>
	/// <param name="key">Encryption key.</param>
	internal Locked(string json, byte[] key) : base() =>
		EncryptedContents = SecretBox.Create(json, Nonce, key);

	/// <summary>
	/// Internal creation only.
	/// </summary>
	/// <param name="json">JSON-encoded contents to encrypt.</param>
	/// <param name="key">Encryption key.</param>
	internal Locked(string json, string key) : base() =>
		EncryptedContents = SecretBox.Create(json, Nonce, HashKey(key));

	/// <summary>
	/// Unlock this box and decrypt contents.
	/// </summary>
	/// <param name="key">Encryption Key.</param>
	/// <result>Unlocked box.</result>
	public Result<Lockable<T>> Unlock(byte[] key) =>
		CryptoF.Unlock(this, key);

	/// <summary>
	/// Unlock this box and decrypt contents.
	/// </summary>
	/// <param name="key">Encryption Key.</param>
	/// <result>Unlocked box.</result>
	public Result<Lockable<T>> Unlock(string key) =>
		CryptoF.Unlock(this, HashKey(key));

	/// <summary>
	/// Serialise this Locked box as JSON.
	/// </summary>
	/// <returns>JSON.</returns>
	public Result<string> Serialise() =>
		EncryptedContents.Match(
			none: () => JsonF.Empty,
			some: x => JsonF.Serialise(this)
		);

	private byte[] HashKey(string key) =>
		GenericHash.Hash(key, Salt, Lockable.KeyLength);
}

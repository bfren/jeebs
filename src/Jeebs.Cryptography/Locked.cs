// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Security.Cryptography;
using System.Text;
using Jeebs.Cryptography.Functions;
using Jeebs.Functions;
using Jeebs.Messages;
using MaybeF;
using Sodium;
using Sodium.Exceptions;

namespace Jeebs.Cryptography;

/// <summary>
/// Contains contents that have been encrypted - see <see cref="Locked{T}.EncryptedContents"/>
/// </summary>
/// <typeparam name="T">Value type</typeparam>
public sealed class Locked<T> : Locked
{
	/// <summary>
	/// Encrypted contents
	/// </summary>
	public Maybe<byte[]> EncryptedContents { get; init; } = F.None<byte[], M.EncryptedContentsNotCreatedYetMsg>();

	/// <summary>
	/// Salt
	/// </summary>
	public byte[] Salt { get; init; }

	/// <summary>
	/// Nonce
	/// </summary>
	public byte[] Nonce { get; init; }

	/// <summary>
	/// Create new Locked box with random salt and nonce
	/// </summary>
	public Locked() =>
		(Salt, Nonce) = (SodiumCore.GetRandomBytes(16), CryptoF.GenerateNonce());

	internal Locked(T contents, byte[] key) : this() =>
		EncryptedContents = JsonF
			.Serialise(
				contents
			)
			.Map(
				x => SecretBox.Create(x, Nonce, key),
				e => new M.CreatingSecretBoxExceptionMsg(e)
			);

	internal Locked(T contents, string key) : this() =>
		EncryptedContents = JsonF
			.Serialise(
				contents
			)
			.Map(
				x => SecretBox.Create(x, Nonce, HashKey(key)),
				e => new M.CreatingSecretBoxExceptionMsg(e)
			);

	/// <summary>
	/// Unlock this LockedBox
	/// </summary>
	/// <param name="key">Encryption Key</param>
	public Maybe<Lockable<T>> Unlock(byte[] key)
	{
		return EncryptedContents.Switch(
			some: x =>
			{
				try
				{
					// Open encrypted contents
					var secret = SecretBox.Open(x, Nonce, key);

					// Deserialise contents and return
					return JsonF
						.Deserialise<T>(
							Encoding.UTF8.GetString(secret)
						)
						.Map(
							x => new Lockable<T>(x),
							F.DefaultHandler
						);
				}
				catch (KeyOutOfRangeException ex)
				{
					return handle(new M.InvalidKeyExceptionMsg(ex));
				}
				catch (NonceOutOfRangeException ex)
				{
					return handle(new M.InvalidNonceExceptionMsg(ex));
				}
				catch (CryptographicException ex)
				{
					return handle(new M.IncorrectKeyOrNonceExceptionMsg(ex));
				}
				catch (Exception ex)
				{
					return handle(new M.UnlockExceptionMsg(ex));
				}
			},
			none: F.None<Lockable<T>, M.UnlockWhenEncryptedContentsIsNoneMsg>()
		);

		// Handle an exception
		static Maybe<Lockable<T>> handle<TMsg>(TMsg ex)
			where TMsg : IExceptionMsg =>
			 F.None<Lockable<T>>(ex);
	}

	/// <summary>
	/// Unlock this LockedBox
	/// </summary>
	/// <param name="key">Encryption Key</param>
	public Maybe<Lockable<T>> Unlock(string key) =>
		Unlock(HashKey(key));

	/// <summary>
	/// Serialise this LockedBox as JSON
	/// </summary>
	public Maybe<string> Serialise() =>
		EncryptedContents.Switch(
			some: _ => JsonF.Serialise(this),
			none: F.Some(JsonF.Empty)
		);

	private byte[] HashKey(string key) =>
		GenericHash.Hash(key, Salt, Lockable.KeyLength);
}

/// <summary>
/// Holds Messages for <see cref="Locked{T}"/>
/// </summary>
public abstract class Locked
{
	/// <summary>
	/// Create object
	/// </summary>
	protected Locked() { }

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>Error creating secret box</summary>
		/// <param name="Value">Exception</param>
		public sealed record class CreatingSecretBoxExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>Encrypted contents not created yet</summary>
		public sealed record class EncryptedContentsNotCreatedYetMsg : Msg;

		/// <summary>Incorrect key or nonce</summary>
		/// <param name="Value">Exception</param>
		public sealed record class IncorrectKeyOrNonceExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>Invalid key</summary>
		/// <param name="Value">Exception</param>
		public sealed record class InvalidKeyExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>Invalid nonce</summary>
		/// <param name="Value">Exception</param>
		public sealed record class InvalidNonceExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>Unlock exception</summary>
		/// <param name="Value">Exception</param>
		public sealed record class UnlockExceptionMsg(Exception Value) : ExceptionMsg;

		/// <summary>Trying to unlock a box without any content</summary>
		public sealed record class UnlockWhenEncryptedContentsIsNoneMsg : Msg;
	}
}

// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Security.Cryptography;
using System.Text;
using Sodium;
using Sodium.Exceptions;
using static F.OptionF;

namespace Jeebs.Cryptography
{
	/// <summary>
	/// Contains contents that have been encrypted - see <see cref="Locked{T}.EncryptedContents"/>
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	public sealed class Locked<T> : Locked
	{
		/// <summary>
		/// Encrypted contents
		/// </summary>
		public Option<byte[]> EncryptedContents { get; init; } = None<byte[], Msg.EncryptedContentsNotCreatedYetMsg>();

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
			(Salt, Nonce) = (SodiumCore.GetRandomBytes(16), F.CryptoF.GenerateNonce());

		internal Locked(T contents, byte[] key) : this() =>
			EncryptedContents = F.JsonF
				.Serialise(
					contents
				)
				.Map(
					x => SecretBox.Create(x, Nonce, key),
					e => new Msg.CreatingSecretBoxExceptionMsg(e)
				);

		internal Locked(T contents, string key) : this() =>
			EncryptedContents = F.JsonF
				.Serialise(
					contents
				)
				.Map(
					x => SecretBox.Create(x, Nonce, HashKey(key)),
					e => new Msg.CreatingSecretBoxExceptionMsg(e)
				);

		/// <summary>
		/// Unlock this LockedBox
		/// </summary>
		/// <param name="key">Encryption Key</param>
		public Option<Lockable<T>> Unlock(byte[] key)
		{
			return EncryptedContents.Switch(
				some: x =>
				{
					try
					{
						// Open encrypted contents
						var secret = SecretBox.Open(x, Nonce, key);

						// Deserialise contents and return
						return F.JsonF
							.Deserialise<T>(
								Encoding.UTF8.GetString(secret)
							)
							.Map(
								x => new Lockable<T>(x),
								DefaultHandler
							);
					}
					catch (KeyOutOfRangeException ex)
					{
						return handle(new Msg.InvalidKeyExceptionMsg(ex));
					}
					catch (NonceOutOfRangeException ex)
					{
						return handle(new Msg.InvalidNonceExceptionMsg(ex));
					}
					catch (CryptographicException ex)
					{
						return handle(new Msg.IncorrectKeyOrNonceExceptionMsg(ex));
					}
					catch (Exception ex)
					{
						return handle(new Msg.UnlockExceptionMsg(ex));
					}
				},
				none: None<Lockable<T>, Msg.UnlockWhenEncryptedContentsIsNoneMsg>()
			);

			// Handle an exception
			static Option<Lockable<T>> handle<TMsg>(TMsg ex)
				where TMsg : IExceptionMsg =>
				None<Lockable<T>>(ex);
		}

		/// <summary>
		/// Unlock this LockedBox
		/// </summary>
		/// <param name="key">Encryption Key</param>
		public Option<Lockable<T>> Unlock(string key) =>
			Unlock(HashKey(key));

		/// <summary>
		/// Serialise this LockedBox as JSON
		/// </summary>
		public Option<string> Serialise() =>
			EncryptedContents.Switch(
				some: _ => F.JsonF.Serialise(this),
				none: Return(F.JsonF.Empty)
			);

		private byte[] HashKey(string key) =>
			GenericHash.Hash(key, Salt, Lockable.KeyLength);
	}

	/// <summary>
	/// Holds Messages for <see cref="Locked{T}"/>
	/// </summary>
	public abstract class Locked
	{
		internal Locked() { }

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Error creating secret box</summary>
			/// <param name="Exception">Exception</param>
			public sealed record class CreatingSecretBoxExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Encrypted contents not created yet</summary>
			public sealed record class EncryptedContentsNotCreatedYetMsg : IMsg { }

			/// <summary>Incorrect key or nonce</summary>
			/// <param name="Exception">Exception</param>
			public sealed record class IncorrectKeyOrNonceExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Invalid key</summary>
			/// <param name="Exception">Exception</param>
			public sealed record class InvalidKeyExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Invalid nonce</summary>
			/// <param name="Exception">Exception</param>
			public sealed record class InvalidNonceExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Unlock exception</summary>
			/// <param name="Exception">Exception</param>
			public sealed record class UnlockExceptionMsg(Exception Exception) : ExceptionMsg(Exception) { }

			/// <summary>Trying to unlock a box without any content</summary>
			public sealed record class UnlockWhenEncryptedContentsIsNoneMsg : IMsg { }
		}
	}
}

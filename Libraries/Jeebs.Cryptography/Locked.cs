using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Jm.Cryptography.Locked;
using Sodium;
using Sodium.Exceptions;

namespace Jeebs.Cryptography
{
	/// <summary>
	/// Contains contents that have been encrypted - see <see cref="Locked{T}.EncryptedContents"/>
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	public sealed class Locked<T>
	{
		/// <summary>
		/// Encrypted contents
		/// </summary>
		public byte[]? EncryptedContents { get; init; }

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
			EncryptedContents = SecretBox.Create(F.JsonF.Serialise(contents), Nonce, key);

		internal Locked(T contents, string key) : this() =>
			EncryptedContents = SecretBox.Create(F.JsonF.Serialise(contents), Nonce, HashKey(key));

		/// <summary>
		/// Unlock this LockedBox
		/// </summary>
		/// <param name="key">Encryption Key</param>
		public Option<Lockable<T>> Unlock(byte[] key)
		{
			if (EncryptedContents is null)
			{
				return Option.None<Lockable<T>>().AddReason<UnlockWhenEncryptedContentsIsNullMsg>();
			}

			try
			{
				// Open encrypted contents
				var secret = SecretBox.Open(EncryptedContents, Nonce, key);

				// Deserialise contents and return
				var json = Encoding.UTF8.GetString(secret);
				return F.JsonF.Deserialise<T>(json).Map(x => new Lockable<T>(x));
			}
			catch (KeyOutOfRangeException ex)
			{
				return handle<InvalidKeyExceptionMsg>(ex);
			}
			catch (NonceOutOfRangeException ex)
			{
				return handle<InvalidNonceExceptionMsg>(ex);
			}
			catch (CryptographicException ex)
			{
				return handle<IncorrectKeyOrNonceMsg>(ex);
			}
			catch (Exception ex)
			{
				return handle<UnlockExceptionMsg>(ex);
			}

			// Handle an exception
			static Option<Lockable<T>> handle<TMsg>(Exception ex)
				where TMsg : IExceptionMsg, new() =>
				Option.None<Lockable<T>>().AddReason<TMsg>(ex);
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
		public string Serialise() =>
			EncryptedContents?.Length switch
			{
				int x when x > 0 =>
					F.JsonF.Serialise(this),

				_ =>
					F.JsonF.Empty
			};

		private byte[] HashKey(string key) =>
			GenericHash.Hash(key, Salt, Lockable.KeyLength);
	}
}

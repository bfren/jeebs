using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Jeebs.Util;
using Jm.Cryptography.Locked;
using Newtonsoft.Json;
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
		public byte[]? EncryptedContents { get; set; }

		/// <summary>
		/// Salt
		/// </summary>
		public byte[] Salt { get; set; }

		/// <summary>
		/// Nonce
		/// </summary>
		public byte[] Nonce { get; set; }

		internal Locked()
			=> (Salt, Nonce) = (SodiumCore.GetRandomBytes(16), F.CryptoF.GenerateNonce());

		internal Locked(T contents, byte[] key) : this()
			=> Fill(contents, key);

		internal Locked(T contents, string key) : this()
			=> Fill(contents, key);

		internal void Fill(T contents, byte[] key)
			=> EncryptedContents = SecretBox.Create(Json.Serialise(contents), Nonce, key);

		internal void Fill(T contents, string key)
			=> Fill(contents, HashKey(key));

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
				return Json.Deserialise<T>(json).Map(x => new Lockable<T>(x));
			}
			catch (KeyOutOfRangeException ex)
			{
				return handle<InvalidKeyExceptionMsg>(ex);
			}
			catch (CryptographicException ex)
			{
				return handle<CryptographicExceptionMsg>(ex);
			}
			catch (JsonException ex)
			{
				return handle<DeserialiseExceptionMsg>(ex);
			}
			catch (Exception ex)
			{
				return handle<UnlockExceptionMsg>(ex);
			}

			// Handle an exception
			static Option<Lockable<T>> handle<TMsg>(Exception ex)
				where TMsg : IExceptionMsg, new()
				=> Option.None<Lockable<T>>().AddReason<TMsg>(ex);
		}

		/// <summary>
		/// Unlock this LockedBox
		/// </summary>
		/// <param name="key">Encryption Key</param>
		public Option<Lockable<T>> Unlock(string key)
			=> Unlock(HashKey(key));

		/// <summary>
		/// Serialise this LockedBox as JSON
		/// </summary>
		public string Serialise()
			=> Json.Serialise(this);

		private byte[] HashKey(string key)
			=> GenericHash.Hash(key, Salt, Lockable.KeyLength);
	}
}

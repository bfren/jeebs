using Jeebs.Util;
using Sodium;
using System;
using System.Text;
using System.Text.Json;

namespace Jeebs.Cryptography
{
	/// <summary>
	/// LockedBox
	/// </summary>
	/// <typeparam name="T">Type of value to be encrypted</typeparam>
	public class LockedBox<T>
	{
		/// <summary>
		/// Salt
		/// </summary>
		public byte[] Salt { get; set; }

		/// <summary>
		/// Nonce
		/// </summary>
		public byte[] Nonce { get; set; }

		/// <summary>
		/// Value to be encrypted
		/// </summary>
		public T Value
		{
#pragma warning disable CS8603 // Possible null reference return.
			get => EncryptedValue is null ? unencryptedValue : default;
#pragma warning restore CS8603 // Possible null reference return.
			set => unencryptedValue = value;
		}

		/// <summary>
		/// Holds the unencrypted value - <see cref="Value"/> only allows access to this value if <see cref="EncryptedValue"/> is null
		/// </summary>
		private T unencryptedValue;

		/// <summary>
		/// Encrypted value
		/// </summary>
		public byte[]? EncryptedValue { get; set; }

		/// <summary>
		/// Whether or not the box is locked
		/// </summary>
		internal bool Locked { get; private set; }

		/// <summary>
		/// Empty constructor
		/// </summary>
#pragma warning disable CS8604 // Possible null reference argument.
		internal LockedBox() : this(default) { }
#pragma warning restore CS8604 // Possible null reference argument.

		/// <summary>
		/// Create new box with contents
		/// </summary>
		/// <param name="value">Box contents</param>
		internal LockedBox(T value)
		{
			Salt = SodiumCore.GetRandomBytes(16);
			Nonce = F.CryptoF.GenerateNonce();
			unencryptedValue = value;
		}

		/// <summary>
		/// Encrypt the box
		/// </summary>
		/// <param name="key">Encryption key</param>
		internal void Lock(byte[] key)
		{
			EncryptedValue = SecretBox.Create(Json.Serialise(Value), Nonce, key);
			Locked = true;
		}

		/// <summary>
		/// Encrypt the box
		/// </summary>
		/// <param name="key">Encryption key</param>
		internal void Lock(string key) => Lock(GenericHash.Hash(key, Salt, 32));

		/// <summary>
		/// Decrypt the box
		/// </summary>
		/// <param name="key">Encryption key</param>
		internal void Unlock(byte[] key)
		{
			// Check EncryptedValue
			if (EncryptedValue == null)
			{
				throw new Jx.Cryptography.DecryptionException($"{nameof(EncryptedValue)} cannot be null - are you decrypting a valid box?");
			}

			// Open the box
			var box = SecretBox.Open(EncryptedValue, Nonce, key);

			// Remove EncryptedValue
			EncryptedValue = null;

			// Decode string and deserialise JSON
			var str = Encoding.UTF8.GetString(box);
			Value = Json.Deserialise<T>(str);

			// Mark as unlocked
			Locked = false;
		}

		/// <summary>
		/// Decrypt the box
		/// </summary>
		/// <param name="key">Encryption key</param>
		internal void Unlock(string key) => Unlock(GenericHash.Hash(key, Salt, 32));

		#region Static

		/// <summary>
		/// Create a box
		/// </summary>
		/// <param name="value">Box contents</param>
		/// <param name="key">Encryption key</param>
		internal static LockedBox<T> Create(T value, byte[] key) => Create(value, b => b.Lock(key));

		/// <summary>
		/// Create a box
		/// </summary>
		/// <param name="value">Box contents</param>
		/// <param name="key">Encryption key</param>
		internal static LockedBox<T> Create(T value, string key) => Create(value, b => b.Lock(key));

		/// <summary>
		/// Create a box
		/// </summary>
		/// <param name="value">Box contents</param>
		/// <param name="lockBox">Action to lock the box</param>
		private static LockedBox<T> Create(T value, Action<LockedBox<T>> lockBox)
		{
			// Value cannot be null
			if (value == null || string.IsNullOrEmpty(value.ToString()))
			{
				throw new ArgumentNullException(nameof(value), "You cannot create a Locked Box with an empty value.");
			}

			// Create and lock box
			var box = new LockedBox<T>(value);
			lockBox(box);

			// Return box
			if (box.Locked)
			{
				return box;
			}
			else
			{
				throw new InvalidOperationException("LockedBox was not locked.");
			}
		}

		/// <summary>
		/// Open a box
		/// </summary>
		/// <param name="json">JSON-serialised Box</param>
		/// <param name="key">Encryption key</param>
		internal static LockedBox<T> Open(string json, byte[] key) => Open(json, box => box.Unlock(key));

		/// <summary>
		/// Open a box
		/// </summary>
		/// <param name="json">JSON-serialised Box</param>
		/// <param name="key">Encryption key</param>
		internal static LockedBox<T> Open(string json, string key) => Open(json, box => box.Unlock(key));

		/// <summary>
		/// Open a box
		/// </summary>
		/// <param name="json">JSON-serialised Box</param>
		/// <param name="unlockBox">Action to unlock the box</param>
		private static LockedBox<T> Open(string json, Action<LockedBox<T>> unlockBox)
		{
			// Holds box
			LockedBox<T> box;

			// Attempt to deserialise the box
			try
			{
				box = Json.Deserialise<LockedBox<T>>(json);
			}
			catch (JsonException ex)
			{
				throw new Jx.Cryptography.DeserialisationException("Unable to open box: invalid JSON", ex);
			}

			// Attempt to deserialise the box contents
			try
			{
				unlockBox(box);
			}
			catch (JsonException ex)
			{
				throw new Jx.Cryptography.DecryptionException($"Unable to decrypt box: invalid JSON", ex);
			}

			return box;
		}

		#endregion
	}
}

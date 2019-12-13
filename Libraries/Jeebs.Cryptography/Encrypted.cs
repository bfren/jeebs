using System;

namespace Jeebs.Cryptography
{
	/// <summary>
	/// Encrypted object
	/// </summary>
	/// <typeparam name="T">Type of value to be encrypted</typeparam>
	public class Encrypted<T> : LockedBox<T>
	{
		/// <summary>
		/// Empty constructor
		/// </summary>
		public Encrypted() { }

		/// <summary>
		/// Construct object
		/// </summary>
		/// <param name="value">Value to encrypt</param>
		/// <param name="key">Encryption key</param>
		public Encrypted(T value, string key) : base(value) => Lock(key);

		/// <summary>
		/// Decrypt value
		/// </summary>
		/// <param name="key">Encryption key</param>
		/// <returns>Decrypted value</returns>
		public T Decrypt(string key)
		{
			Unlock(key);
			return Value;
		}
	}
}

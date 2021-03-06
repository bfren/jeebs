// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

namespace Jeebs.Cryptography
{
	/// <summary>
	/// Constants relating to <see cref="Lockable{T}"/> and <see cref="Locked{T}"/>
	/// </summary>
	public static class Lockable
	{
		/// <summary>
		/// Length of encryption key (if it's a byte array)
		/// </summary>
		public const int KeyLength = 32;
	}

	/// <summary>
	/// Contains contents that can been encrypted
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	public sealed class Lockable<T>
	{
		/// <summary>
		/// Contents
		/// </summary>
		public T Contents { get; }

		/// <summary>
		/// Create object
		/// </summary>
		/// <param name="contents">Contents</param>
		public Lockable(T contents) =>
			Contents = contents;

		/// <summary>
		/// Lock object
		/// </summary>
		/// <param name="key">Encryption key - must be <see cref="Lockable.KeyLength"/> bytes</param>
		public Locked<T> Lock(byte[] key) =>
			key.Length switch
			{
				Lockable.KeyLength =>
					new Locked<T>(Contents, key),

				_ =>
					throw new Jx.Cryptography.InvalidKeyLengthException()
			};

		/// <summary>
		/// Lock object
		/// </summary>
		/// <param name="key">Encryption key</param>
		public Locked<T> Lock(string key) =>
			new(Contents, key);
	}
}

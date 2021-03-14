// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using static F.OptionF;

namespace Jeebs.Cryptography
{
	/// <summary>
	/// Contains contents that can been encrypted
	/// </summary>
	/// <typeparam name="T">Value type</typeparam>
	public sealed class Lockable<T> : Lockable
	{
		/// <summary>
		/// Contents
		/// </summary>
		public T Contents { get; private init; }

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
		public Option<Locked<T>> Lock(byte[] key) =>
			key.Length switch
			{
				Lockable.KeyLength =>
					new Locked<T>(Contents, key),

				_ =>
					None<Locked<T>, Msg.InvalidKeyLengthMsg>()
			};

		/// <summary>
		/// Lock object
		/// </summary>
		/// <param name="key">Encryption key</param>
		public Locked<T> Lock(string key) =>
			new(Contents, key);
	}

	/// <summary>
	/// Holds constants and Messages for <see cref="Lockable{T}"/>
	/// </summary>
	public abstract class Lockable
	{
		/// <summary>
		/// Length of encryption key (if it's a byte array)
		/// </summary>
		public const int KeyLength = 32;

		internal Lockable() { }

		/// <summary>Messages</summary>
		public static class Msg
		{
			/// <summary>Encryption key is not the correct length to lock the box</summary>
			public sealed record InvalidKeyLengthMsg : IMsg { }
		}
	}
}

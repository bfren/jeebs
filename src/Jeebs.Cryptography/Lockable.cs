// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Messages;
using Maybe;
using Maybe.Functions;

namespace Jeebs.Cryptography;

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
	public Maybe<Locked<T>> Lock(byte[] key) =>
		key.Length switch
		{
			int l when l == KeyLength =>
				new Locked<T>(Contents, key),

			_ =>
				MaybeF.None<Locked<T>, M.InvalidKeyLengthMsg>()
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
	public static readonly int KeyLength = 32;

	/// <summary>
	/// Create object
	/// </summary>
	protected Lockable() { }

	/// <summary>Messages</summary>
	public static class M
	{
		/// <summary>Encryption key is not the correct length to lock the box</summary>
		public sealed record class InvalidKeyLengthMsg : Msg;
	}
}

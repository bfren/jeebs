// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;
using MaybeF;
using MaybeF.Linq;

namespace Jeebs.Cryptography;

/// <summary>
/// <see cref="object"/> Extensions
/// </summary>
public static class ObjectExtensions
{
	/// <summary>
	/// Encrypt an object using the specified key and return it serialised as JSON
	/// </summary>
	/// <typeparam name="T">Type of object being encrypted</typeparam>
	/// <param name="this">Value to encrypt</param>
	/// <param name="key">Encryption Key (must be 32 bytes)</param>
	public static Maybe<string> Encrypt<T>(this T @this, byte[] key) =>
		@this switch
		{
			T x =>
				from l in new Lockable<T>(x).Lock(key)
				from s in l.Serialise()
				select s,

			_ =>
				JsonF.Empty
		};

	/// <summary>
	/// Encrypt an object using the specified key and return it serialised as JSON
	/// </summary>
	/// <typeparam name="T">Type of object being encrypted</typeparam>
	/// <param name="this">Value to encrypt</param>
	/// <param name="key">Encryption key</param>
	public static Maybe<string> Encrypt<T>(this T @this, string key) =>
		@this switch
		{
			T x =>
				from s in new Lockable<T>(x).Lock(key).Serialise()
				select s,

			_ =>
				JsonF.Empty
		};
}

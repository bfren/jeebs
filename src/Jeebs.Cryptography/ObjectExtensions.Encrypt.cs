// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Cryptography.Functions;
using Jeebs.Functions;

namespace Jeebs.Cryptography;

public static partial class ObjectExtensions
{$1/// <summary>
$2/// $3$4.
$5/// </summary>
	/// <typeparam name="T">Type of object being encrypted.</typeparam>
	/// <param name="this">Value to encrypt.</param>
	/// <param name="key">Encryption Key (must be 32 bytes).</param>
	/// <returns>JSON serialised encrypted box.</returns>
	public static Result<string> Encrypt<T>(this T @this, byte[] key) =>
		@this switch
		{
			T x =>
				from l in CryptoF.Lock(@this, key)
				from s in l.Serialise()
				select s,

			_ =>
				JsonF.Empty
		};

	/// <inheritdoc cref="Encrypt{T}(T, byte[])"/>
	public static Result<string> Encrypt<T>(this T @this, string key) =>
		@this switch
		{
			T x =>
				from l in CryptoF.Lock(@this, key)
				from s in l.Serialise()
				select s,

			_ =>
				JsonF.Empty
		};
}

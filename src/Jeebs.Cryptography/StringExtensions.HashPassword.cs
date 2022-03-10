// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Sodium;

namespace Jeebs.Cryptography;

public static partial class StringExtensions
{
	/// <summary>
	/// Hash a password using argon2id - returns hash containing the salt (of length 128 bytes)
	/// </summary>
	/// <param name="this">Password to hash</param>
	public static string HashPassword(this string @this)
	{
		if (string.IsNullOrEmpty(@this))
		{
			return string.Empty;
		}

		return PasswordHash.ArgonHashString(@this, PasswordHash.StrengthArgon.Moderate);
	}
}

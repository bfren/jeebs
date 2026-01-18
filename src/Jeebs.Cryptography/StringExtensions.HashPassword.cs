// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Linq;
using Sodium;

namespace Jeebs.Cryptography;

public static partial class StringExtensions
{
	/// <summary>
	/// Hash a password using argon2id - returns hash containing the salt (of length 128 bytes).
	/// </summary>
	/// <param name="this">Password to hash</param>
	public static string HashPassword(this string @this)
	{
		// Return empty string
		if (string.IsNullOrWhiteSpace(@this))
		{
			return string.Empty;
		}

		// Hash string and then remove null characters
		var hash = PasswordHash
			.ArgonHashString(@this, PasswordHash.StrengthArgon.Moderate)
			.Where(c => c != char.MinValue)
			.ToArray();

		// Return as a string
		return new(hash);
	}
}

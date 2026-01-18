// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Sodium;

namespace Jeebs.Cryptography;

public static partial class StringExtensions
{
	/// <summary>
	/// Verify a password hashed using argon2id.
	/// </summary>
	/// <param name="this">Password hash</param>
	/// <param name="password">Password to verify</param>
	public static bool VerifyPassword(this string @this, string password)
	{
		if (string.IsNullOrWhiteSpace(@this))
		{
			return false;
		}

		if (string.IsNullOrEmpty(password))
		{
			return false;
		}

		return PasswordHash.ArgonHashStringVerify(@this.Trim(), password);
	}
}

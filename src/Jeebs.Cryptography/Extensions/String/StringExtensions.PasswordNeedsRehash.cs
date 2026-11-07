// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Sodium;

namespace Jeebs.Cryptography;

public static partial class StringExtensions
{
	/// <summary>
	/// Returns true if a password hash was not created with the settings <see cref="HashPassword"/> uses now,
	/// so the password should be hashed again (e.g. after the next successful sign in).
	/// </summary>
	/// <param name="this">Password hash.</param>
	public static bool PasswordNeedsRehash(this string @this) =>
		string.IsNullOrWhiteSpace(@this) || PasswordHash.ArgonPasswordNeedsRehash(@this.Trim(), PasswordStrength);
}

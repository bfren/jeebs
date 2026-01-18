// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Security.Cryptography;

namespace Jeebs.Auth.Totp.Functions;

public static partial class TotpF
{
	/// <summary>
	/// Compute a hash using HMAC SHA-256 algorithm.
	/// </summary>
	/// <param name="key">Hash key.</param>
	/// <param name="counter">Hash counter.</param>
	public static byte[] ComputeHash(byte[] key, byte[] counter)
	{
		var hasher = new HMACSHA256(key);
		return hasher.ComputeHash(counter);
	}
}

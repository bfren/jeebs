// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Sodium;

namespace Jeebs.Cryptography.Functions;

public static partial class CryptoF
{
	/// <summary>
	/// Generate a 32 byte key to use for encryption.
	/// </summary>
	/// <returns>Encryption key as byte array.</returns>
	public static Result<byte[]> GenerateKey() =>
		R.Try(SecretBox.GenerateKey, e => R.Fail(nameof(CryptoF), nameof(GenerateKey), e));
}

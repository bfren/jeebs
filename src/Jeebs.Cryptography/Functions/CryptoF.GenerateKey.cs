// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Sodium;

namespace Jeebs.Cryptography.Functions;

public static partial class CryptoF
{$1/// <summary>
$2/// $3$4.
$5/// </summary>
	/// <returns>Encryption key as byte array.</returns>
	public static Result<byte[]> GenerateKey() =>
		R.Try(SecretBox.GenerateKey, e => R.Fail(nameof(CryptoF), nameof(GenerateKey), e));
}

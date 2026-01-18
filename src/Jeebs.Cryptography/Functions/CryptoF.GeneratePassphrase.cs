// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using RndF;

namespace Jeebs.Cryptography.Functions;

public static partial class CryptoF
{$1/// <summary>
$2/// $3$4.
$5/// </summary>
	/// <returns>Passphrase.</returns>
	public static string GeneratePassphrase() =>
		Rnd.StringF.Passphrase();$1/// <summary>
$2/// $3$4.
$5/// </summary>
	/// <param name="numberOfWords">The number of words in the passphrase (minimum: 5).</param>
	/// <returns>Passphrase.</returns>
	public static Result<string> GeneratePassphrase(int numberOfWords) =>
		numberOfWords switch
		{
			>= 5 =>
				R.Try(Rnd.StringF.Passphrase, e => R.Fail(nameof(CryptoF), nameof(GeneratePassphrase), e)),

			_ =>
				R.Fail(nameof(CryptoF), nameof(GeneratePassphrase),
					"Cryptographically secure passphrases must contain at least five words ({Number} requested).", numberOfWords
				)
		};
}

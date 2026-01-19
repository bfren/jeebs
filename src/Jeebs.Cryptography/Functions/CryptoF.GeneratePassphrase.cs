// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using RndF;

namespace Jeebs.Cryptography.Functions;

public static partial class CryptoF
{
	/// <summary>
	/// Generate an 8-word passphrase.
	/// </summary>
	/// <returns>Passphrase.</returns>
	public static Result<string> GeneratePassphrase() =>
		R.Try(
			Rnd.StringF.Passphrase,
			e => R.Fail(nameof(CryptoF), nameof(GeneratePassphrase), e)
		);

	/// <summary>
	/// Generate a passphrase.
	/// </summary>
	/// <param name="numberOfWords">The number of words in the passphrase (minimum: 3).</param>
	/// <returns>Passphrase.</returns>
	public static Result<string> GeneratePassphrase(int numberOfWords) =>
		numberOfWords switch
		{
			>= 3 =>
				R.Try(
					() => Rnd.StringF.Passphrase(numberOfWords),
					e => R.Fail(nameof(CryptoF), nameof(GeneratePassphrase), e)
				),

			_ =>
				R.Fail(nameof(CryptoF), nameof(GeneratePassphrase),
					"Cryptographically secure passphrases must contain at least three words ({Number} requested).", numberOfWords
				)
		};
}

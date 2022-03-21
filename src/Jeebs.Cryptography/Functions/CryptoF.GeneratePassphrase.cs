// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Messages;
using RndF;

namespace Jeebs.Cryptography.Functions;

public static partial class CryptoF
{
	/// <summary>
	/// Generate an 8-word passphrase
	/// </summary>
	public static Maybe<string> GeneratePassphrase() =>
		Rnd.StringF.Passphrase();

	/// <summary>
	/// Generate a passphrase
	/// </summary>
	/// <param name="numberOfWords">The number of words in the passphrase (minimum: 5)</param>
	public static Maybe<string> GeneratePassphrase(int numberOfWords) =>
		numberOfWords switch
		{
			>= 5 =>
				Rnd.StringF.Passphrase(numberOfWords),

			_ =>
				F.None<string>(new M.CryptographicallySecurePassphrasesMustContainAtLeastFiveWordsMsg(numberOfWords))
		};

	public static partial class M
	{
		/// <summary>EFF specify that with the long word list, at least five words must be used to be secure</summary>
		/// <param name="Value">Number of words</param>
		public sealed record class CryptographicallySecurePassphrasesMustContainAtLeastFiveWordsMsg(int Value) : WithValueMsg<int>;
	}
}

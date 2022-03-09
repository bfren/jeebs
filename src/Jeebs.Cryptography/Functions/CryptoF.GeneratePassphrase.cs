// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Messages;
using Jeebs.Random;
using Maybe;
using Maybe.Functions;

namespace Jeebs.Cryptography.Functions;

public static partial class CryptoF
{
	/// <summary>
	/// Generate a 3-word passphrase
	/// </summary>
	public static Maybe<string> GeneratePassphrase() =>
		GeneratePassphrase(3);

	/// <summary>
	/// Generate a passphrase
	/// </summary>
	/// <param name="numberOfWords">The number of words in the passphrase (minimum: 2)</param>
	public static Maybe<string> GeneratePassphrase(int numberOfWords) =>
		Rnd.StringF.Passphrase(numberOfWords) switch
		{
			string pass =>
				MaybeF.Some(pass),

			_ =>
				MaybeF.None<string, M.NullPassphraseMsg>()
		};

	public static partial class M
	{
		/// <summary>Generated passphrase was null</summary>
		public sealed record class NullPassphraseMsg : Msg;
	}
}

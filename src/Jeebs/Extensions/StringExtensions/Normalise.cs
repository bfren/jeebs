// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.RegularExpressions;

namespace Jeebs;

public static partial class StringExtensions
{
	/// <summary>
	/// Normalise a string by making it lowercase, stripping all non-letters and replacing spaces with -
	/// </summary>
	/// <param name="this">String to perform operation on</param>
	public static string Normalise(this string @this) =>
		Modify(@this, () =>
		{
			// Make lowercase, and remove non-letters characters
			var nonNormalisedCharacters = new Regex("[^a-z -]");
			var normalised = nonNormalisedCharacters.Replace(@this.ToLowerInvariant(), "").Trim();

			// Remove hyphens from the start of the string
			normalised = normalised.TrimStart('-');

			// Replace multiple spaces and hyphens with a single hyphen
			var multipleSpacesAndHyphens = new Regex("[ -]+");
			return multipleSpacesAndHyphens.Replace(normalised, "-");
		});
}

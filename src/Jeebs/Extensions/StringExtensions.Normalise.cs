// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.RegularExpressions;

namespace Jeebs.Extensions;

public static partial class StringExtensions
{
	/// <summary>
	/// Normalise a string by making it lowercase, stripping all non-letters and replacing spaces with '-'.
	/// </summary>
	/// <param name="this">Input string.</param>
	/// <returns>Normalised string.</returns>
	public static string Normalise(this string @this) =>
		Modify(@this, () =>
		{
			// Make lowercase, and remove non-letters characters
			var normalised = UnwantedCharactersRegex()
				.Replace(@this.ToLowerInvariant(), "")
				.Trim();

			// Trim hyphens from the start and end of the string
			normalised = normalised
				.Trim('-');

			// Replace multiple spaces and hyphens with a single hyphen
			normalised = MultipleSpacesAndHyphensRegex()
				.Replace(normalised, "-");

			return normalised;
		});

	[GeneratedRegex("[^a-z -]")]
	private static partial Regex UnwantedCharactersRegex();

	[GeneratedRegex("[ -]+")]
	private static partial Regex MultipleSpacesAndHyphensRegex();
}

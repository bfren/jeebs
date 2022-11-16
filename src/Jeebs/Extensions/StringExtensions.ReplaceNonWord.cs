// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.RegularExpressions;

namespace Jeebs.Extensions;

public static partial class StringExtensions
{
	/// <summary>
	/// Replace non-word characters in a string, useful for creating HTML IDs (for example)
	/// </summary>
	/// <param name="this">String to perform operation on</param>
	/// <param name="replaceWith">String to replace unwanted characters with</param>
	public static string ReplaceNonWord(this string @this, string? replaceWith) =>
		Modify(@this, () =>
		{
			// Make sure replaceWith isn't null
			if (replaceWith is null)
			{
				replaceWith = string.Empty;
			}

			// Now replace all non-word characters
			var nonWord = NonWordCharactersRegex();
			return nonWord.Replace(@this, replaceWith);
		});

	/// <inheritdoc cref="ReplaceNonWord(string, string?)"/>
	public static string ReplaceNonWord(this string @this) =>
		ReplaceNonWord(@this, null);

	[GeneratedRegex("\\W+")]
	private static partial Regex NonWordCharactersRegex();
}

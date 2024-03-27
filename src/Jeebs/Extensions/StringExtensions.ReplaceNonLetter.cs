// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.RegularExpressions;

namespace Jeebs.Extensions;

public static partial class StringExtensions
{
	/// <inheritdoc cref="ReplaceNonWord(string, string?)"/>
	public static string ReplaceNonLetter(this string @this) =>
		ReplaceNonWord(@this, string.Empty);

	/// <summary>
	/// Replace non-letter characters in a string.
	/// </summary>
	/// <param name="this">String to perform operation on.</param>
	/// <param name="with">String to replace unwanted characters with.</param>
	/// <returns><paramref name="this"/> with non-word characters replaced by <paramref name="with"/>.</returns>
	public static string ReplaceNonLetter(this string @this, string with) =>
		Modify(@this, () => NonLetterCharactersRegex().Replace(@this, with ?? string.Empty));

	[GeneratedRegex("[^a-zA-Z]+")]
	private static partial Regex NonLetterCharactersRegex();
}

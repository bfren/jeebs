// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.RegularExpressions;

namespace Jeebs;

public static partial class StringExtensions
{
	/// <inheritdoc cref="ReplaceNonWord(string, string?)"/>
	public static string ReplaceNonWord(this string @this) =>
		ReplaceNonWord(@this, string.Empty);

	/// <summary>
	/// Replace non-word characters in a string, useful for creating HTML IDs (for example).
	/// </summary>
	/// <param name="this">String to perform operation on.</param>
	/// <param name="with">String to replace unwanted characters with.</param>
	/// <returns><paramref name="this"/> with non-word characters replaced by <paramref name="with"/>.</returns>
	public static string ReplaceNonWord(this string @this, string with) =>
		Modify(@this, () => NonWordCharactersRegex().Replace(@this, with ?? string.Empty));

	[GeneratedRegex("\\W+")]
	private static partial Regex NonWordCharactersRegex();
}

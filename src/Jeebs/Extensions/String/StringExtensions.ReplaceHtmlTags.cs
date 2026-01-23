// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.RegularExpressions;

namespace Jeebs;

public static partial class StringExtensions
{
	/// <inheritdoc cref="ReplaceHtmlTags(string, string)"/>
	public static string ReplaceHtmlTags(this string @this) =>
		ReplaceHtmlTags(@this, string.Empty);

	/// <summary>
	/// Replace all HTML tags.
	/// </summary>
	/// <param name="this">Input string.</param>
	/// <param name="with">String to replace HTML tags with.</param>
	/// <returns><paramref name="this"/> with HTML tags replaced by <paramref name="with"/>.</returns>
	public static string ReplaceHtmlTags(this string @this, string with) =>
		Modify(@this, () => HtmlCharactersRegex().Replace(@this, with ?? string.Empty));

	[GeneratedRegex("<.*?>")]
	private static partial Regex HtmlCharactersRegex();
}

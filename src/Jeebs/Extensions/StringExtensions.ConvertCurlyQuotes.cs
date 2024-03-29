// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.RegularExpressions;

namespace Jeebs.Extensions;

public static partial class StringExtensions
{
	/// <summary>
	/// Convert straight quotes to curly quotes
	/// </summary>
	/// <param name="this">Input string</param>
	/// <param name="ls">Left single quote mark</param>
	/// <param name="rs">Right single quote mark</param>
	/// <param name="ld">Left double quote mark</param>
	/// <param name="rd">Right double quote mark</param>
	public static string ConvertCurlyQuotes(this string @this, string ls, string rs, string ld, string rd) =>
		Modify(@this, () =>
		{
			// Simple replace
			var s = @this
				.Replace("&#34;", "\"")
				.Replace("&#39;", "'");

			// Use regular expression to replace single quotes at the end of words
			var singleQuote = SingleQuoteRegex();
			s = singleQuote.Replace(s, $"$1{ls}").Replace("'", rs);

			// Use regular expression to replace double quote at the end of words
			var doubleQuote = DoubleQuoteRegex();
			return doubleQuote.Replace(s, $"$1{ld}").Replace("\"", rd);
		});

	/// <inheritdoc cref="ConvertCurlyQuotes(string, string, string, string, string)"/>
	public static string ConvertCurlyQuotes(this string @this, string ls, string rs, string ld) =>
		ConvertCurlyQuotes(@this, ls, rs, ld, "”");

	/// <inheritdoc cref="ConvertCurlyQuotes(string, string, string, string, string)"/>
	public static string ConvertCurlyQuotes(this string @this, string ls, string rs) =>
		ConvertCurlyQuotes(@this, ls, rs, "“", "”");

	/// <inheritdoc cref="ConvertCurlyQuotes(string, string, string, string, string)"/>
	public static string ConvertCurlyQuotes(this string @this, string ls) =>
		ConvertCurlyQuotes(@this, ls, "’", "“", "”");

	/// <inheritdoc cref="ConvertCurlyQuotes(string, string, string, string, string)"/>
	public static string ConvertCurlyQuotes(this string @this) =>
		ConvertCurlyQuotes(@this, "‘", "’", "“", "”");

	[GeneratedRegex("(\\s|^)'")]
	private static partial Regex SingleQuoteRegex();

	[GeneratedRegex("(\\s|^)\"")]
	private static partial Regex DoubleQuoteRegex();
}

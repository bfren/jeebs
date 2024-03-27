// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;
using System.Text.RegularExpressions;

namespace Jeebs.Extensions;

public static partial class StringExtensions
{
	/// <summary>
	/// Convert straight quotes to curly quotes, but not inside HTML tags / attributes.
	/// </summary>
	/// <param name="this">Input string.</param>
	/// <returns><paramref name="this"/> with straight quotes converted to curly quotes except inside HTML.</returns>
	public static string ConvertInnerHtmlQuotes(this string @this) =>
		Modify(@this, () =>
		{
			// Convert using HTML entities
			static string convert(string input) =>
				input.ConvertCurlyQuotes(ls: "&lsquo;", rs: "&rsquo;", ld: "&ldquo;", rd: "&rdquo;");

			// Match HTML tags and their attributes
			var htmlTags = HtmlTagsRegex();

			// Get the first match
			var match = htmlTags.Match(@this);

			// Loop through each HTML tag, replacing the quotes in the text in between
			var lastIndex = 0;
			var builder = new StringBuilder();
			while (match.Success)
			{
				// Replace text between this match and the previous one
				var textBetween = @this[lastIndex..match.Index];
				_ = builder.Append(convert(textBetween));

				// Add the text in this section unchanged
				_ = builder.Append(match.Value);

				// Move to the next section
				lastIndex = match.Index + match.Length;
				match = match.NextMatch();
			}

			// Replace any remaining quotes
			var remaining = @this[lastIndex..];
			_ = builder.Append(convert(remaining));

			// Return result string
			return builder.ToString();
		});

	[GeneratedRegex("<[^>]*>")]
	private static partial Regex HtmlTagsRegex();
}

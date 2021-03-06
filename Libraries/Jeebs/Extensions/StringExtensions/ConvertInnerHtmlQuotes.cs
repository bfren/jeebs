// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

using System.Text;
using System.Text.RegularExpressions;

namespace Jeebs
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Convert straight quotes to curly quotes, but not inside HTML tags / attributes
		/// </summary>
		/// <param name="this">Input string</param>
		/// <returns>IHtmlContent with curly quotes</returns>
		public static string ConvertInnerHtmlQuotes(this string @this) =>
			Modify(@this, () =>
			{
				// Convert using HTML entities
				static string convert(string input)
				{
					return input.ConvertCurlyQuotes(ls: "&lsquo;", rs: "&rsquo;", ld: "&ldquo;", rd: "&rdquo;");
				}

				// Match HTML tags and their attributes
				var htmlTags = new Regex("<[^>]*>");

				// Get the first match
				Match match = htmlTags.Match(@this);

				// Loop through each HTML tag, replacing the quotes in the text in between
				int lastIndex = 0;
				var builder = new StringBuilder();
				while (match.Success)
				{
					// Replace text between this match and the previous one
					string textBetween = @this[lastIndex..match.Index];
					builder.Append(convert(textBetween));

					// Add the text in this section unchanged
					builder.Append(match.Value);

					// Move to the next section
					lastIndex = match.Index + match.Length;
					match = match.NextMatch();
				}

				// Replace any remaining quotes
				string remaining = @this[lastIndex..];
				builder.Append(convert(remaining));

				// Return result string
				return builder.ToString();
			});
	}
}

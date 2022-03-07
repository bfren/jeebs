// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.RegularExpressions;

namespace Jeebs;

public static partial class StringExtensions
{
	/// <summary>
	/// Replace all HTML tags
	/// </summary>
	/// <param name="this">The input string</param>
	/// <param name="replaceWith">String to replace HTML tags with</param>
	public static string ReplaceHtmlTags(this string @this, string? replaceWith) =>
		Modify(@this, () =>
		{
			// Make sure replaceWith isn't null
			if (replaceWith is null)
			{
				replaceWith = string.Empty;
			}

			// Now replace all HTML characters
			var re = new Regex("<.*?>");
			return re.Replace(@this, replaceWith);
		});

	/// <inheritdoc cref="ReplaceHtmlTags(string, string?)"/>
	public static string ReplaceHtmlTags(this string @this) =>
		ReplaceHtmlTags(@this, null);
}

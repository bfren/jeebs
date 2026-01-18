// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text.RegularExpressions;
using Jeebs.Extensions;

namespace Jeebs.WordPress.ContentFilters;

/// <summary>
/// Generate Excerpt.
/// </summary>
public sealed partial class GenerateExcerpt : ContentFilter
{
	/// <inheritdoc/>
	private GenerateExcerpt(Func<string, string> filter) : base(filter) { }

	/// <summary>
	/// Create filter with default max length
	/// </summary>
	public static ContentFilter Create() =>
		Create(200);

	/// <summary>
	/// Create filter
	/// </summary>
	/// <param name="maxLength">Maximum length of excerpt - ignored if <!--more--> is present</param>
	public static ContentFilter Create(int maxLength) =>
		new GenerateExcerpt(content =>
		{
			// If there's nothing there, return an empty string
			if (string.IsNullOrEmpty(content))
			{
				return string.Empty;
			}

			// Strip out square brackets
			var squareBrackets1 = SquareBrackets1Regex();
			content = squareBrackets1.Replace(content, " ");

			var squareBrackets2 = SquareBrackets2Regex();
			content = squareBrackets2.Replace(content, " ");

			// Strip out new lines
			var newLines = NewLinesRegex();
			content = newLines.Replace(content, " ");

			// Cut out everything after <!--more--> tag
			var alreadyCut = false;
			if (content.IndexOf("<!--more-->", StringComparison.InvariantCulture) is int more && more > 0)
			{
				content = content[..more];
				alreadyCut = true;
			}

			// Replace HTML tags
			content = content.ReplaceHtmlTags(" ");

			// Strip out multiple spaces and trim
			var multipleSpaces = MultipleSpacesRegex();
			content = multipleSpaces.Replace(content, " ").Trim();

			// Ensure maximum length
			if (!alreadyCut)
			{
				content = content.NoLongerThan(maxLength);
			}

			// Return filtered content
			return content;
		});

	[GeneratedRegex("\\[(\\w+) *(.*)\\](.*)(\\[\\/(\\1)\\])", RegexOptions.Singleline)]
	private static partial Regex SquareBrackets1Regex();

	[GeneratedRegex("\\[(\\w+) *(.*)\\]", RegexOptions.Singleline)]
	private static partial Regex SquareBrackets2Regex();

	[GeneratedRegex("[\\n\\r]")]
	private static partial Regex NewLinesRegex();

	[GeneratedRegex("\\s+")]
	private static partial Regex MultipleSpacesRegex();
}

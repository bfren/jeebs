// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.RegularExpressions;

namespace Jeebs.Extensions;

public static partial class StringExtensions
{
	/// <summary>
	/// Ensure a string contains only numbers
	/// </summary>
	/// <param name="this">The input string</param>
	/// <param name="replaceWith">String to replace non-numerical characters with</param>
	public static string ReplaceNonNumerical(this string @this, string? replaceWith) =>
		Modify(@this, () =>
		{
			// Make sure replaceWith isn't null
			if (replaceWith is null)
			{
				replaceWith = string.Empty;
			}

			// Now replace all non-numerical characters
			var nonNumerical = NonNumericalCharactersRegex();
			return nonNumerical.Replace(@this, replaceWith);
		});

	/// <inheritdoc cref="ReplaceNonNumerical(string, string?)"/>
	public static string ReplaceNonNumerical(this string @this) =>
		ReplaceNonNumerical(@this, null);

	[GeneratedRegex("[^0-9]+")]
	private static partial Regex NonNumericalCharactersRegex();
}

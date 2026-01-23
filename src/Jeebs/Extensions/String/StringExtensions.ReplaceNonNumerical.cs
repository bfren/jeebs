// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.RegularExpressions;

namespace Jeebs;

public static partial class StringExtensions
{
	/// <inheritdoc cref="ReplaceNonNumerical(string, string?)"/>
	public static string ReplaceNonNumerical(this string @this) =>
		ReplaceNonNumerical(@this, string.Empty);

	/// <summary>
	/// Ensure a string contains only numbers.
	/// </summary>
	/// <param name="this">Input string.</param>
	/// <param name="with">String to replace non-numerical characters with.</param>
	/// <returns><paramref name="this"/> with non-numerical characters replaced by <paramref name="with"/>.</returns>
	public static string ReplaceNonNumerical(this string @this, string with) =>
		Modify(@this, () => NonNumericalCharactersRegex().Replace(@this, with ?? string.Empty));

	[GeneratedRegex("[^0-9]+")]
	private static partial Regex NonNumericalCharactersRegex();
}

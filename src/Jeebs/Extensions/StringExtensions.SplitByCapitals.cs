// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text.RegularExpressions;

namespace Jeebs.Extensions;

public static partial class StringExtensions
{
	/// <summary>
	/// Split a CamelCase string by capital letters.
	/// </summary>
	/// <param name="this">Input string.</param>
	/// <returns><paramref name="this"/> split by capital letters.</returns>
	public static string SplitByCapitals(this string @this) =>
		Modify(@this, () => CapitalLettersRegex().Replace(@this, " $2").Trim());

	[GeneratedRegex("( *)([A-Z])")]
	private static partial Regex CapitalLettersRegex();
}

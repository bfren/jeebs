// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Extensions;

public static partial class StringExtensions
{
	/// <summary>
	/// Decode HTML entities.
	/// </summary>
	/// <param name="this">Input string.</param>
	/// <returns>HTML decoded string.</returns>
	public static string HtmlDecode(this string @this) =>
		Modify(@this, () => System.Net.WebUtility.HtmlDecode(@this));
}

// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

public static partial class StringExtensions
{
	/// <summary>
	/// Decode HTML entities
	/// </summary>
	/// <param name="this">Input string</param>
	public static string HtmlDecode(this string @this) =>
		Modify(@this, () => System.Net.WebUtility.HtmlDecode(@this));
}

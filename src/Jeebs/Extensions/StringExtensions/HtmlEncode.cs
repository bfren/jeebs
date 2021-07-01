// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Encode HTML entities
		/// </summary>
		/// <param name="this">Input string</param>
		/// <returns>String with HTML entities encoded</returns>
		public static string HtmlEncode(this string @this) =>
			Modify(@this, () => System.Net.WebUtility.HtmlEncode(@this));
	}
}

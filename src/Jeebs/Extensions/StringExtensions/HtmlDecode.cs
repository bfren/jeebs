// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

namespace Jeebs
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Decode HTML entities
		/// </summary>
		/// <param name="this">Input string</param>
		/// <returns>String with HTML entities decoded</returns>
		public static string HtmlDecode(this string @this) =>
			Modify(@this, () => System.Net.WebUtility.HtmlDecode(@this));
	}
}

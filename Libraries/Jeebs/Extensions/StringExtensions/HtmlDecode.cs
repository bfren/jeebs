// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

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

using System;
using System.Collections.Generic;
using System.Text;

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

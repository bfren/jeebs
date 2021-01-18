using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Jeebs.Reflection;

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

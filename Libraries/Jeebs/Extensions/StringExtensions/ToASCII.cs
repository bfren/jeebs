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
		/// Return the input string encoded into ASCII Html Entities
		/// Warning: this only works with ASCII 'Printable' characters (32->126), NOT 'Extended' characters
		/// </summary>
		/// <param name="this">The input string</param>
		/// <returns>ASCII-encoded String</returns>
		public static string ToASCII(this string @this) =>
			Modify(@this, () =>
			{
				// Get ASCII encoding and convert byte by byte
				byte[] a = Encoding.ASCII.GetBytes(@this);

				var encoded = new StringBuilder();
				foreach (byte b in a)
				{
					encoded.AppendFormat("&#{0};", b);
				}

				return encoded.ToString();
			});
	}
}

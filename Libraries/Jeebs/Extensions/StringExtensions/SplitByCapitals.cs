using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Jeebs
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Split a CamelCase string by capitals
		/// </summary>
		/// <param name="this">String object</param>
		/// <returns>String split by capital letters</returns>
		public static string SplitByCapitals(this string @this) =>
			Modify(@this, () => Regex.Replace(@this, "( *)([A-Z])", " $2").Trim());
	}
}

// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

namespace Jeebs
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Equivalent of PHP lcfirst() - makes the first character of a string lowercase
		/// </summary>
		/// <param name="this">String object</param>
		/// <returns>String, with the first letter forced to Lowercase</returns>
		public static string ToLowerFirst(this string @this) =>
			Modify(@this, () => char.ToLower(@this[0]) + @this[1..]);
	}
}

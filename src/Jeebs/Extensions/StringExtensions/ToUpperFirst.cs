// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.uk/2013

namespace Jeebs
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Equivalent of PHP ucfirst() - makes the first character of a string uppercase
		/// </summary>
		/// <param name="this">String object</param>
		/// <returns>String, with the first letter forced to Uppercase</returns>
		public static string ToUpperFirst(this string @this) =>
			Modify(@this, () => char.ToUpper(@this[0]) + @this[1..]);
	}
}

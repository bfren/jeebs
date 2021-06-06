// Jeebs Rapid Application Development
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

namespace Jeebs
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Equivalent of PHP ucfirst() - except it lowers the case of all subsequent letters as well
		/// </summary>
		/// <param name="this">String object</param>
		/// <returns>String, with the first letter forced to Uppercase</returns>
		public static string ToSentenceCase(this string @this) =>
			Modify(@this, () => char.ToUpper(@this[0]) + @this[1..].ToLowerInvariant());
	}
}

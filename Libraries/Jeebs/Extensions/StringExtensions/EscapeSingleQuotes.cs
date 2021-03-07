// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

namespace Jeebs
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Escape all single quotes (when outputting text into a Javascript string)
		/// </summary>
		/// <param name="this">String to escape</param>
		/// <returns>Escaped string</returns>
		public static string EscapeSingleQuotes(this string @this) =>
			Modify(@this, () => @this.Replace("'", @"\'"));
	}
}

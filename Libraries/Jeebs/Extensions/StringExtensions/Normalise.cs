using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Jeebs
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Normalise a string by making it lowercase, stripping all non-letters and replacing spaces with -
		/// </summary>
		/// <param name="this">String to perform operation on</param>
		/// <returns>Normalised string</returns>
		public static string Normalise(this string @this) =>
			Modify(@this, () =>
			{
				// Make lowercase, and remove non-letters characters
				string normalised = Regex.Replace(@this.ToLowerInvariant(), "[^a-z -]", "").Trim();

				// Remove hyphens from the start of the string
				normalised = normalised.TrimStart('-');

				// Replace multiple spaces and hyphens with a single hyphen
				return Regex.Replace(normalised, "[ -]+", "-");
			});
	}
}

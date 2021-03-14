// Jeebs Rapid Application Development
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Text.RegularExpressions;

namespace Jeebs
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Ensure a string contains only numbers
		/// </summary>
		/// <param name="this">The input string</param>
		/// <param name="replaceWith">String to replace non-numerical characters with</param>
		/// <returns>Input string with all non-numerical characters removed</returns>
		public static string ReplaceNonNumerical(this string @this, string? replaceWith) =>
			Modify(@this, () =>
			{
				// Make sure replaceWith isn't null
				if (replaceWith == null)
				{
					replaceWith = string.Empty;
				}

				// Now replace all non-numerical characters
				return Regex.Replace(@this, "[^0-9]+", replaceWith);
			});

		/// <inheritdoc cref="ReplaceNonNumerical(string, string?)"/>
		public static string ReplaceNonNumerical(this string @this) =>
			ReplaceNonNumerical(@this, null);
	}
}

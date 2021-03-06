// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

namespace Jeebs
{
	public static partial class StringExtensions
	{
		/// <summary>
		/// Ensure a string is no longer than the specified maximum
		/// </summary>
		/// <param name="this">Input string</param>
		/// <param name="maxLength">The maximum length of the string</param>
		/// <param name="continuation">The continuation string to append to strings longer than the maximum</param>
		/// <param name="empty">Text to return if the primary string is empty</param>
		/// <returns>Modified input string</returns>
		public static string NoLongerThan(this string @this, int maxLength, string continuation, string? empty) =>
			Modify(@this, () =>
				(maxLength > 0 && @this.Length > maxLength) switch
				{
					true =>
						@this.Substring(0, maxLength) + continuation,

					false =>
						@this
				},
				empty
			);

		/// <inheritdoc cref="NoLongerThan(string, int, string, string?)"/>
		public static string NoLongerThan(this string @this, int maxLength, string continuation) =>
			NoLongerThan(@this, maxLength, continuation, null);

		/// <inheritdoc cref="NoLongerThan(string, int, string, string?)"/>
		public static string NoLongerThan(this string @this, int maxLength) =>
			NoLongerThan(@this, maxLength, "..", null);
	}
}

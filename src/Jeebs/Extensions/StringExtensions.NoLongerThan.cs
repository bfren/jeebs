// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Extensions;

public static partial class StringExtensions
{
	/// <summary>
	/// Ensure a string is no longer than the specified maximum
	/// </summary>
	/// <param name="this">Input string</param>
	/// <param name="maxLength">The maximum length of the string</param>
	/// <param name="continuation">The continuation string to append to strings longer than the maximum</param>
	/// <param name="empty">Text to return if the primary string is empty</param>
	public static string NoLongerThan(this string @this, int maxLength, string continuation, string? empty) =>
		Modify(@this, () =>
			(maxLength > 0 && @this.Length > maxLength) switch
			{
				true =>
					@this[..maxLength] + continuation,

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

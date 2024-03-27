// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Extensions;

public static partial class StringExtensions
{
	/// <summary>
	/// Trim a string from the start of another string.
	/// </summary>
	/// <param name="this">Input string.</param>
	/// <param name="value">Value to trim.</param>
	/// <returns><paramref name="this"/> with <paramref name="value"/> trimmed from the start.</returns>
	public static string TrimStart(this string @this, string value) =>
		@this.StartsWith(value, StringComparison.InvariantCulture) switch
		{
			true =>
				@this.Remove(@this.IndexOf(value, StringComparison.InvariantCulture)),

			false =>
				@this
		};
}

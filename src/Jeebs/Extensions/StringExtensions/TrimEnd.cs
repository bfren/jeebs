// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs;

public static partial class StringExtensions
{
	/// <summary>
	/// Trim a string from the end of another string
	/// </summary>
	/// <param name="this">String object</param>
	/// <param name="value">Value to trim</param>
	public static string TrimEnd(this string @this, string value) =>
		@this.EndsWith(value, StringComparison.InvariantCulture) switch
		{
			true =>
				@this.Remove(@this.LastIndexOf(value, StringComparison.InvariantCulture)),

			false =>
				@this
		};
}

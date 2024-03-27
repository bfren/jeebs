// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Extensions;

public static partial class StringExtensions
{
	/// <summary>
	/// Escape all single quotes (useful when outputting text into a Javascript string).
	/// </summary>
	/// <param name="this">String to escape.</param>
	/// <returns><paramref name="this"/> with all single-quote characters escaped.</returns>
	public static string EscapeSingleQuotes(this string @this) =>
		Modify(@this, () => @this.Replace("'", @"\'"));
}

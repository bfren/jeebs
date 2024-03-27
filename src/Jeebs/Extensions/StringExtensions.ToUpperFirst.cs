// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Extensions;

public static partial class StringExtensions
{
	/// <summary>
	/// Equivalent of PHP ucfirst() - makes the first character of a string uppercase.
	/// </summary>
	/// <param name="this">Input string.</param>
	/// <returns><paramref name="this"/> with first character in upper case.</returns>
	public static string ToUpperFirst(this string @this) =>
		Modify(@this, () => char.ToUpperInvariant(@this[0]) + @this[1..]);
}

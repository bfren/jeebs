// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

public static partial class StringExtensions
{
	/// <summary>
	/// Equivalent of PHP lcfirst() - makes the first character of a string lowercase.
	/// </summary>
	/// <param name="this">Input string.</param>
	/// <returns><paramref name="this"/> with first character in lower case.</returns>
	public static string ToLowerFirst(this string @this) =>
		Modify(@this, () => char.ToLowerInvariant(@this[0]) + @this[1..]);
}

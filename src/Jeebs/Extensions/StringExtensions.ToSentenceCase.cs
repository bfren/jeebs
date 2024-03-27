// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Extensions;

public static partial class StringExtensions
{
	/// <summary>
	/// Equivalent of PHP ucfirst() - except it lowers the case of all subsequent letters as well.
	/// </summary>
	/// <param name="this">Input string.</param>
	/// <returns><paramref name="this"/> with first character in upper case and all others in lower case.</returns>
	public static string ToSentenceCase(this string @this) =>
		Modify(@this, () => char.ToUpperInvariant(@this[0]) + @this[1..].ToLowerInvariant());
}

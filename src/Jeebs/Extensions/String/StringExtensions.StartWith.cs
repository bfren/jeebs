// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Globalization;

namespace Jeebs;

public static partial class StringExtensions
{
	/// <summary>
	/// Ensure that an input string starts with a single defined character.
	/// </summary>
	/// <param name="this">The input string.</param>
	/// <param name="character">The character to start the string with.</param>
	/// <returns><paramref name="this"/> starting with <paramref name="character"/>.</returns>
	public static string StartWith(this string @this, char character) =>
		Modify(@this, () => string.Format(CultureInfo.InvariantCulture, "{0}{1}", character, @this.TrimStart(character)));

	/// <summary>
	/// Ensure that an input string starts with another string.
	/// </summary>
	/// <param name="this">The input string.</param>
	/// <param name="value">The string to start the string with.</param>
	/// <returns><paramref name="this"/> starting with <paramref name="value"/>.</returns>
	public static string StartWith(this string @this, string value) =>
		Modify(@this, () => string.Format(CultureInfo.InvariantCulture, "{0}{1}", value, @this.TrimStart(value)));
}

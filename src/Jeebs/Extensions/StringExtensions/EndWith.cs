// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

public static partial class StringExtensions
{
	/// <summary>
	/// Ensure that an input string ends with a single defined character
	/// </summary>
	/// <param name="this">The input string</param>
	/// <param name="character">The character to end the string with</param>
	/// <returns>The input string ending with a single 'character'</returns>
	public static string EndWith(this string @this, char character) =>
		Modify(@this, () => string.Format("{0}{1}", @this.TrimEnd(character), character));

	/// <summary>
	/// Ensure that an input string ends with another string
	/// </summary>
	/// <param name="this">The input string</param>
	/// <param name="value">The string to end the string with</param>
	/// <returns>The input string ending with string 'value'</returns>
	public static string EndWith(this string @this, string value) =>
		Modify(@this, () => string.Format("{0}{1}", @this.TrimEnd(value), value));
}

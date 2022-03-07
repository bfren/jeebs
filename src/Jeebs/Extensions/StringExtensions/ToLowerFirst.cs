// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Globalization;

namespace Jeebs;

public static partial class StringExtensions
{
	/// <summary>
	/// Equivalent of PHP lcfirst() - makes the first character of a string lowercase
	/// </summary>
	/// <param name="this">String object</param>
	/// <returns>String, with the first letter forced to Lowercase</returns>
	public static string ToLowerFirst(this string @this) =>
		Modify(@this, () => char.ToLower(@this[0], CultureInfo.InvariantCulture) + @this[1..]);

	/// <inheritdoc cref="ToLowerFirst(string)"/>
	public static string ToCamelCase(this string @this) =>
		ToLowerFirst(@this);
}

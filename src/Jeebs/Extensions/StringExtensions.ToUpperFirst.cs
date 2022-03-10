// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Globalization;

namespace Jeebs.Extensions;

public static partial class StringExtensions
{
	/// <summary>
	/// Equivalent of PHP ucfirst() - makes the first character of a string uppercase
	/// </summary>
	/// <param name="this">String object</param>
	public static string ToUpperFirst(this string @this) =>
		Modify(@this, () => char.ToUpper(@this[0], CultureInfo.InvariantCulture) + @this[1..]);

	/// <inheritdoc cref="ToUpperFirst(string)"/>
	public static string ToPascalCase(this string @this) =>
		ToUpperFirst(@this);
}

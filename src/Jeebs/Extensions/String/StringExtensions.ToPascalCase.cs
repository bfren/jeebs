// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

public static partial class StringExtensions
{
	/// <summary>
	/// Converts a string to pascal case.
	/// </summary>
	/// <remarks>
	/// Pascal Case = NumberOfDoughnuts
	/// </remarks>
	/// <seealso cref="ToCamelCase(string)"/>
	/// <seealso cref="ToKebabCase(string)"/>
	/// <seealso cref="ToSnakeCase(string)"/>
	/// <param name="this">Input string.</param>
	/// <returns><paramref name="this"/> in PascalCase.</returns>
	public static string ToPascalCase(this string @this) =>
		Modify(@this, () => @this.ToTitleCase().ReplaceNonLetter());
}

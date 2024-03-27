// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Extensions;

public static partial class StringExtensions
{
	/// <summary>
	/// Converts a string to snake case.
	/// </summary>
	/// <remarks>
	/// Snake Case = number_of_doughnuts
	/// </remarks>
	/// <seealso cref="ToCamelCase(string)"/>
	/// <seealso cref="ToKebabCase(string)"/>
	/// <seealso cref="ToPascalCase(string)"/>
	/// <param name="this">Input string.</param>
	/// <returns><paramref name="this"/> in snake_case.</returns>
	public static string ToSnakeCase(this string @this) =>
		Modify(@this, () => @this.ReplaceNonLetter("_").ToLowerInvariant());
}

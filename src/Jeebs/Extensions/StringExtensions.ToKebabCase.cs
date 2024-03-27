// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Extensions;

public static partial class StringExtensions
{
	/// <summary>
	/// Converts a string to kebab case.
	/// </summary>
	/// <remarks>
	/// Kebab Case = number-of-doughnuts
	/// </remarks>
	/// <seealso cref="ToCamelCase(string)"/>
	/// <seealso cref="ToPascalCase(string)"/>
	/// <seealso cref="ToSnakeCase(string)"/>
	/// <param name="this">Input string.</param>
	/// <returns><paramref name="this"/> in kebab-case.</returns>
	public static string ToKebabCase(this string @this) =>
		Modify(@this, () => @this.ReplaceNonLetter("-").ToLowerInvariant());
}

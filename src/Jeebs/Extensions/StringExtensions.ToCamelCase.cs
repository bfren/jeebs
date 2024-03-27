// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Extensions;

public static partial class StringExtensions
{
	/// <summary>
	/// Converts a string to camel case.
	/// </summary>
	/// <remarks>
	/// Camel Case = numberOfDoughnuts
	/// </remarks>
	/// <seealso cref="ToKebabCase(string)"/>
	/// <seealso cref="ToPascalCase(string)"/>
	/// <seealso cref="ToSnakeCase(string)"/>
	/// <param name="this">Input string.</param>
	/// <returns><paramref name="this"/> in camelCase.</returns>
	public static string ToCamelCase(this string @this) =>
		Modify(@this, () => @this.ToTitleCase().ReplaceNonLetter().ToLowerFirst());
}

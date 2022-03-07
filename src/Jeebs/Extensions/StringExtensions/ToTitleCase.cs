// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Globalization;

namespace Jeebs;

public static partial class StringExtensions
{
	/// <summary>
	/// Converts a string to Title Case (ignoring acronyms)
	/// </summary>
	/// <param name="this">String object</param>
	/// <returns>String converted to Title Case</returns>
	public static string ToTitleCase(this string @this) =>
		Modify(@this, () =>
		{
			var array = @this.ToCharArray();

			// Handle the first letter in the string.
			if (array.Length >= 1 && char.IsLower(array[0]))
			{
				array[0] = char.ToUpper(array[0], CultureInfo.InvariantCulture);
			}

			// Scan through the letters, checking for spaces.
			// ... Uppercase the lowercase letters following spaces.
			for (var i = 1; i < array.Length; i++)
			{
				if (array[i - 1] == ' ' && char.IsLower(array[i]))
				{
					array[i] = char.ToUpper(array[i], CultureInfo.InvariantCulture);
				}
			}

			return new string(array);
		});
}

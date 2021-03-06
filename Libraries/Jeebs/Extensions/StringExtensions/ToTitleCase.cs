// Copyright (c) bcg|design.
// Licensed under https://mit.bcgdesign.com/2013.

namespace Jeebs
{
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
				char[] array = @this.ToCharArray();

				// Handle the first letter in the string.
				if (array.Length >= 1 && char.IsLower(array[0]))
				{
					array[0] = char.ToUpper(array[0]);
				}

				// Scan through the letters, checking for spaces.
				// ... Uppercase the lowercase letters following spaces.
				for (int i = 1; i < array.Length; i++)
				{
					if (array[i - 1] == ' ' && char.IsLower(array[i]))
					{
						array[i] = char.ToUpper(array[i]);
					}
				}

				return new string(array);
			});
	}
}

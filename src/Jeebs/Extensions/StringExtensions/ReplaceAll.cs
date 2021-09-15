// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

public static partial class StringExtensions
{
	/// <summary>
	/// Replace all strings in an array
	/// </summary>
	/// <param name="this">String to perform operation on</param>
	/// <param name="replace">Array of strings to replace</param>
	/// <param name="with">String to replace occurrences with</param>
	/// <returns>String with all strings in the array replaced</returns>
	public static string ReplaceAll(this string @this, string[] replace, string with) =>
		Modify(@this, () =>
		{
			// Copy string and replace values
			string r = @this;
			foreach (string t in replace)
			{
				r = r.Replace(t, with);
			}

			return r;
		});
}

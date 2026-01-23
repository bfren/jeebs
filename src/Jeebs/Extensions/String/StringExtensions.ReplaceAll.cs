// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs;

public static partial class StringExtensions
{
	/// <summary>
	/// Replace all strings in an array with a single value.
	/// </summary>
	/// <param name="this">Input string.</param>
	/// <param name="replace">Array of strings to replace.</param>
	/// <param name="with">String to replace occurrences with.</param>
	/// <returns><paramref name="this"/> with <paramref name="replace"/> values replaced by <paramref name="with"/>.</returns>
	public static string ReplaceAll(this string @this, string[] replace, string with) =>
		Modify(@this, () =>
		{
			// Copy string and replace values
			var r = @this;
			foreach (var t in replace)
			{
				r = r.Replace(t, with);
			}

			return r;
		});
}

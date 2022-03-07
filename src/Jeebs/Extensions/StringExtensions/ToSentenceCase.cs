// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Globalization;

namespace Jeebs;

public static partial class StringExtensions
{
	/// <summary>
	/// Equivalent of PHP ucfirst() - except it lowers the case of all subsequent letters as well
	/// </summary>
	/// <param name="this">String object</param>
	public static string ToSentenceCase(this string @this) =>
		Modify(@this, () => char.ToUpper(@this[0], CultureInfo.InvariantCulture) + @this[1..].ToLowerInvariant());
}

// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Extensions;

public static partial class StringExtensions
{
	/// <summary>
	/// Return null if the string is empty or null - otherwise, return the string
	/// </summary>
	/// <param name="this">String object</param>
	public static string NullIfEmpty(this string @this) =>
		Modify(@this, () => @this);
}

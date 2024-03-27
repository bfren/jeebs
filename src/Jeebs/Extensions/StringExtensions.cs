// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;

namespace Jeebs.Extensions;

/// <summary>
/// Extension methods for <see cref="string"/> objects.
/// </summary>
public static partial class StringExtensions
{
	/// <inheritdoc cref="Modify(string, Func{string?}, string)"/>
	private static string Modify(string s, Func<string?> perform) =>
		Modify(s, perform, string.Empty);

	/// <summary>
	/// Perform a modification on an input string.
	/// </summary>
	/// <param name="s">Input string.</param>
	/// <param name="perform">Function to modify and return the input string.</param>
	/// <param name="empty">String to return if <paramref name="s"/> is empty.</param>
	/// <returns>Modified string (or <paramref name="empty"/> if <paramref name="s"/> is null or empty).</returns>
	private static string Modify(string s, Func<string?> perform, string empty) =>
		string.IsNullOrEmpty(s) switch
		{
			false =>
				perform() ?? empty,

			true =>
				empty
		};
}

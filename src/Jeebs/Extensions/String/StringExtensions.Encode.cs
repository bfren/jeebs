// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text;

namespace Jeebs;

public static partial class StringExtensions
{
	/// <summary>
	/// Encode an input string as Base64.
	/// </summary>
	/// <param name="this">Input string.</param>
	/// <returns>Base64 encoded string.</returns>
	public static string Encode(this string @this) =>
		Modify(@this, () =>
		{
			var bytes = Encoding.UTF8.GetBytes(@this);
			return Convert.ToBase64String(bytes);
		});
}

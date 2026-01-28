// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Text;

namespace Jeebs;

public static partial class StringExtensions
{
	/// <summary>
	/// Decode a Base64 string.
	/// </summary>
	/// <param name="this">Base64 string.</param>
	/// <returns>Plain string.</returns>
	public static string Decode(this string @this) =>
		Modify(@this, () =>
		{
			var bytes = Convert.FromBase64String(@this);
			return Encoding.UTF8.GetString(bytes);
		});
}

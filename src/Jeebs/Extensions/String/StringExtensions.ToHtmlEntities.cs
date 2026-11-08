// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;

namespace Jeebs;

public static partial class StringExtensions
{
	/// <summary>
	/// Return the input string with every character encoded as a numeric HTML entity.
	/// </summary>
	/// <param name="this">Input string.</param>
	/// <returns>HTML entity-encoded string.</returns>
	public static string ToHtmlEntities(this string @this) =>
		Modify(@this, () =>
		{
			// Encode each Unicode code point, so characters outside the BMP (e.g. emoji) are one entity
			var encoded = new StringBuilder();
			foreach (var rune in @this.EnumerateRunes())
			{
				_ = encoded.Append("&#").Append(rune.Value).Append(';');
			}

			return encoded.ToString();
		});
}

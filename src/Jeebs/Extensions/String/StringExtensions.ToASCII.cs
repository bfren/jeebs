// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Globalization;
using System.Text;

namespace Jeebs;

public static partial class StringExtensions
{
	/// <summary>
	/// Return the input string encoded into ASCII Html Entities.
	/// </summary>
	/// <remarks>
	/// Warning: this only works with ASCII 'Printable' characters (32->126), NOT 'Extended' characters.
	/// </remarks>
	/// <param name="this">Input string.</param>
	/// <returns>ASCII-encoded string.</returns>
	public static string ToASCII(this string @this) =>
		Modify(@this, () =>
		{
			// Get ASCII encoding and convert byte by byte
			var a = Encoding.ASCII.GetBytes(@this);

			var encoded = new StringBuilder();
			foreach (var b in a)
			{
				_ = encoded.AppendFormat(CultureInfo.InvariantCulture, "&#{0};", b);
			}

			return encoded.ToString();
		});
}

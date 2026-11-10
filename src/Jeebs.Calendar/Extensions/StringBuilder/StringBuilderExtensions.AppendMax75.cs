// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;

namespace Jeebs.Calendar;

internal static partial class StringBuilderExtensions
{
	/// <summary>
	/// Append a line to <paramref name="this"/>, splitting at 75 characters according to
	/// https://icalendar.org/iCalendar-RFC-5545/3-1-content-lines.html.
	/// </summary>
	/// <param name="this">StringBuilder.</param>
	/// <param name="text">String to append.</param>
	internal static void AppendMax75(this StringBuilder @this, string text)
	{
		// Maximum line length - must be 74 to handle the single whitespace
		// character on subsequent lines
		const int max = 74;

		// If text is short enough, simply append it
		if (text.Length <= max)
		{
			_ = @this.Append(text).Append(NewLine);
			return;
		}

		// Split at 75
		var remaining = text;
		var first = true;
		do
		{
			// Get next slice
			var next = remaining[0..max];
			remaining = remaining[max..];

			// Append line - after the first line, subsequent lines must begin with
			// a single whitespace character - either space or tab
			if (first)
			{
				_ = @this.Append(next).Append(NewLine);
				first = false;
			}
			else
			{
				_ = @this.Append(' ').Append(next).Append(NewLine);
			}
		} while (remaining.Length > max);

		// Append remaining text
		_ = @this.Append(' ').Append(remaining).Append(NewLine);
	}

	/// <summary>
	/// RFC 5545 lines end with CRLF on every platform.
	/// </summary>
	internal const string NewLine = "\r\n";
}

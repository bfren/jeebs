// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;

namespace Jeebs.Calendar;

/// <summary>
/// StringBuilder extensions
/// </summary>
internal static class StringBuilderExtensions
{
	/// <summary>
	/// Append a line to <paramref name="this"/>, splitting at 75 characters according to
	/// https://icalendar.org/iCalendar-RFC-5545/3-1-content-lines.html
	/// </summary>
	/// <param name="this"></param>
	/// <param name="text"></param>
	internal static void AppendMax75(this StringBuilder @this, string text)
	{
		// Maximum line length - must be 74 to handle the single whitespace
		// character on subsequent lines
		const int max = 74;

		// If text is short enough, simply append it
		if (text.Length < max)
		{
			@this.AppendLine(text);
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
				@this.AppendLine(next);
				first = false;
			}
			else
			{
				@this.AppendLine(" " + next);
			}
		} while (remaining.Length > max);

		// Append remaining text
		@this.AppendLine(" " + remaining);
	}
}

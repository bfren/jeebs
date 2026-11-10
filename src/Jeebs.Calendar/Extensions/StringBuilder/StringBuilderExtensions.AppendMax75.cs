// Jeebs Rapid Application Development
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;

namespace Jeebs.Calendar;

internal static partial class StringBuilderExtensions
{
	/// <summary>
	/// Append a line to <paramref name="this"/>, folding at 75 octets (UTF-8 bytes) according to
	/// https://icalendar.org/iCalendar-RFC-5545/3-1-content-lines.html, without splitting a character.
	/// </summary>
	/// <param name="this">StringBuilder.</param>
	/// <param name="text">String to append.</param>
	internal static void AppendMax75(this StringBuilder @this, string text)
	{
		// Maximum bytes of text per line - 74 leaves room for the single whitespace
		// character that begins each folded line
		const int max = 74;

		var bytes = 0;
		var index = 0;
		foreach (var rune in text.EnumerateRunes())
		{
			// Fold before a character that would take the line over the limit
			if (bytes + rune.Utf8SequenceLength > max)
			{
				_ = @this.Append(NewLine).Append(' ');
				bytes = 0;
			}

			_ = @this.Append(text, index, rune.Utf16SequenceLength);
			bytes += rune.Utf8SequenceLength;
			index += rune.Utf16SequenceLength;
		}

		_ = @this.Append(NewLine);
	}

	/// <summary>
	/// RFC 5545 lines end with CRLF on every platform.
	/// </summary>
	internal const string NewLine = "\r\n";
}

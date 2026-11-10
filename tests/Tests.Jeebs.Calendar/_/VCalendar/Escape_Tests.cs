// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Calendar.Models;

namespace Jeebs.Calendar.VCalendar_Tests;

public class Escape_Tests
{
	[Theory]
	[InlineData("plain", "plain")]
	[InlineData(@"a\b", @"a\\b")]
	[InlineData("a;b", @"a\;b")]
	[InlineData("a,b", @"a\,b")]
	[InlineData("a\nb", @"a\nb")]
	[InlineData("a\r\nb", @"a\nb")]
	[InlineData("a\\;b,c\r\nd", @"a\\\;b\,c\nd")]
	public void Escapes_Text(string input, string expected)
	{
		// Arrange

		// Act
		var result = VCalendar.Escape(input);

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public void GetEvent__Escapes_Description_And_Location()
	{
		// Arrange
		var e = new EventModel(Rnd.DateTime, Rnd.DateTime, false, Rnd.Str, "line one\nline two", "Room 1, Building 2", false);

		// Act
		var result = VCalendar.GetEvent(Rnd.DateTime, Rnd.Str, Rnd.Str, e);

		// Assert
		Assert.Contains("\r\nDESCRIPTION:line one\\nline two\r\n", result);
		Assert.Contains("\r\nLOCATION:Room 1\\, Building 2\r\n", result);
	}
}

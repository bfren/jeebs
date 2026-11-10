// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

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
	public void Null__Returns_Empty()
	{
		// Arrange

		// Act
		var result = VCalendar.Escape(null);

		// Assert
		Assert.Empty(result);
	}
}

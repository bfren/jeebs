// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Extensions;

namespace Jeebs.StringExtensions_Tests;

public class StartWith_Tests
{
	[Theory]
	[InlineData("")]
	public void NullOrEmpty_ReturnsOriginal(string input)
	{
		// Arrange

		// Act
		var r0 = input.StartWith(Rnd.Char);
		var r1 = input.StartWith(Rnd.Str);

		// Assert
		Assert.Equal(input, r0);
		Assert.Equal(input, r1);
	}

	[Theory]
	[InlineData("en", 'B', "Ben")]
	[InlineData("Ben", 'B', "Ben")]
	[InlineData("ben", 'B', "Bben")]
	public void String_ReturnsValueStartingWithCharacter(string input, char startWith, string expected)
	{
		// Arrange

		// Act
		var result = input.StartWith(startWith);

		// Assert
		Assert.Equal(expected, result);
	}
}

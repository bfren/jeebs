// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Extensions;

namespace Jeebs.StringExtensions_Tests;

public class ToTitleCase_Tests
{
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	public void NullOrEmpty_ReturnsOriginal(string input)
	{
		// Arrange

		// Act
		var result = input.ToTitleCase();

		// Assert
		Assert.Equal(input, result);
	}

	[Theory]
	[InlineData("this is a test sentence", "This Is A Test Sentence")]
	[InlineData("testing The PHP acronym", "Testing The PHP Acronym")]
	public void String_ReturnsValueInTitleCase(string input, string expected)
	{
		// Arrange

		// Act
		var result = input.ToTitleCase();

		// Assert
		Assert.Equal(expected, result);
	}
}

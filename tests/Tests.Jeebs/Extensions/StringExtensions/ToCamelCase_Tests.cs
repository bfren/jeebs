// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.StringExtensions_Tests;

public class ToCamelCase_Tests
{
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	public void NullOrEmpty_ReturnsOriginal(string input)
	{
		// Arrange

		// Act
		var result = input.ToCamelCase();

		// Assert
		Assert.Equal(input, result);
	}

	[Theory]
	[InlineData("Ben", "ben")]
	[InlineData("bEN", "bEN")]
	public void String_ReturnsValueWithLowercaseFirstLetter(string input, string expected)
	{
		// Arrange

		// Act
		var result = input.ToCamelCase();

		// Assert
		Assert.Equal(expected, result);
	}
}

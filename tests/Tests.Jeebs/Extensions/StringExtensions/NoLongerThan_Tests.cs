// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.StringExtensions_Tests;

public class NoLongerThan_Tests
{
	[Theory]
	[InlineData("")]
	public void NullOrEmpty_ReturnsOriginal(string input)
	{
		// Arrange

		// Act
		var result = input.NoLongerThan(10);

		// Assert
		Assert.Equal(input, result);
	}

	[Theory]
	[InlineData("123", 4, null, null, "123")]
	[InlineData("1234", 4, null, null, "1234")]
	[InlineData("12345", 4, "..", null, "1234..")]
	[InlineData("12345", 4, null, null, "1234")]
	public void String_ReturnsTruncatedValue(string input, int max, string? continuation, string? empty, string expected)
	{
		// Arrange

		// Act
		var result = input.NoLongerThan(max, continuation!, empty!);

		// Assert
		Assert.Equal(expected, result);
	}
}

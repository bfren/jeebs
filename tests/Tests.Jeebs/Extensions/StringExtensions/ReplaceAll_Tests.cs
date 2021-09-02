// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.StringExtensions_Tests;

public class ReplaceAll_Tests
{
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	public void NullOrEmpty_ReturnsOriginal(string input)
	{
		// Arrange

		// Act
		var result = input.ReplaceAll(Array.Empty<string>(), string.Empty);

		// Assert
		Assert.Equal(input, result);
	}

	[Theory]
	[InlineData("Ben Green", new[] { "e", "n" }, null, "B Gr")]
	[InlineData("Ben Green", new[] { "e", "n" }, "-", "B-- Gr---")]
	public void String_ReturnsValueWithStringsReplaced(string input, string[] replace, string with, string expected)
	{
		// Arrange

		// Act
		var result = input.ReplaceAll(replace, with);

		// Assert
		Assert.Equal(expected, result);
	}
}

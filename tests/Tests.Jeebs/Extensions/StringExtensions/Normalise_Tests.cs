// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Extensions;

namespace Jeebs.StringExtensions_Tests;

public class Normalise_Tests
{
	[Theory]
	[InlineData("")]
	public void NullOrEmpty_ReturnsOriginal(string input)
	{
		// Arrange

		// Act
		var result = input.Normalise();

		// Assert
		Assert.Equal(input, result);
	}

	[Theory]
	[InlineData("&$G54F*FH(3)FKASD63&£asdf", "gffhfkasdasdf")]
	[InlineData("one two three", "one-two-three")]
	[InlineData("one-two-three", "one-two-three")]
	[InlineData(" one  two   three    ", "one-two-three")]
	[InlineData("1-two three", "two-three")]
	public void String_ReturnsNormalisedValue(string input, string expcted)
	{
		// Arrange

		// Act
		var result = input.Normalise();

		// Assert
		Assert.Equal(expcted, result);
	}
}

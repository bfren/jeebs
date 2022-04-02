﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Extensions;

namespace Jeebs.StringExtensions_Tests;

public class ToUpperFirst_Tests
{
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	public void NullOrEmpty_ReturnsOriginal(string input)
	{
		// Arrange

		// Act
		var result = input.ToUpperFirst();

		// Assert
		Assert.Equal(input, result);
	}

	[Theory]
	[InlineData("bEN", "BEN")]
	[InlineData("Ben", "Ben")]
	public void String_ReturnsValueWithUppercaseFirstLetter(string input, string expected)
	{
		// Arrange

		// Act
		var result = input.ToUpperFirst();

		// Assert
		Assert.Equal(expected, result);
	}
}

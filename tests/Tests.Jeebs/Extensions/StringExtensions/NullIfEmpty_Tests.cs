﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Extensions;

namespace Jeebs.StringExtensions_Tests;

public class NullIfEmpty_Tests
{
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	public void NullOrEmpty_ReturnsOriginal(string input)
	{
		// Arrange

		// Act
		var result = input.NullIfEmpty();

		// Assert
		Assert.Equal(input, result);
	}

	[Theory]
	[InlineData("Ben")]
	public void String_ReturnsOriginalValue(string input)
	{
		// Arrange

		// Act
		var result = input.NullIfEmpty();

		// Assert
		Assert.Equal(input, result);
	}
}

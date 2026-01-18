// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Extensions;

namespace Jeebs.StringExtensions_Tests;

public class ReplaceNonWord_Tests
{
	[Theory]
	[InlineData("")]
	public void NullOrEmpty_ReturnsOriginal(string input)
	{
		// Arrange

		// Act
		var result = input.ReplaceNonWord();

		// Assert
		Assert.Equal(input, result);
	}

	[Theory]
	[InlineData(" {B)e(n_ G}re $%en&", null, "Ben_Green")]
	[InlineData("B!n_Gr@#en", "e", "Ben_Green")]
	[InlineData(" {B)e(n_ G}re $%en&", "-", "-B-e-n_-G-re-en-")]
	public void String_ReturnsValueWithNonWordCharactersReplaced(string input, string? with, string expected)
	{
		// Arrange

		// Act
		var result = input.ReplaceNonWord(with!);

		// Assert
		Assert.Equal(expected, result);
	}
}

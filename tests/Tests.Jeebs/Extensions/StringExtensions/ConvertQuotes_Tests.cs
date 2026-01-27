// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.StringExtensions_Tests;

public class ConvertQuotes_Tests
{
	[Theory]
	[InlineData("")]
	public void NullOrEmpty_ReturnsOriginal(string input)
	{
		// Arrange

		// Act
		var result = input.ConvertCurlyQuotes();

		// Assert
		Assert.Equal(input, result);
	}

	[Theory]
	[InlineData("'Ben'", "‘Ben’")]
	[InlineData("'Ben' 'Green'", "‘Ben’ ‘Green’")]
	[InlineData("'Ben's Test'", "‘Ben’s Test’")]
	[InlineData("\"Ben\"", "“Ben”")]
	[InlineData("\"Ben\" \"Green\"", "“Ben” “Green”")]
	[InlineData("\"Ben's Test\"", "“Ben’s Test”")]
	public void String_ReturnsValueWithQuotesConverted(string input, string expected)
	{
		// Arrange

		// Act
		var result = input.ConvertCurlyQuotes();

		// Assert
		Assert.Equal(expected, result);
	}
}

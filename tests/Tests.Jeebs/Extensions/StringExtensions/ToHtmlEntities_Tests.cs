// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.StringExtensions_Tests;

public class ToHtmlEntities_Tests
{
	[Theory]
	[InlineData("")]
	public void NullOrEmpty_ReturnsOriginal(string input)
	{
		// Arrange

		// Act
		var result = input.ToHtmlEntities();

		// Assert
		Assert.Equal(input, result);
	}

	[Theory]
	[InlineData("Ben Green", "&#66;&#101;&#110;&#32;&#71;&#114;&#101;&#101;&#110;")]
	[InlineData("&<>#'{$^?~", "&#38;&#60;&#62;&#35;&#39;&#123;&#36;&#94;&#63;&#126;")]
	[InlineData("£ü©§", "&#163;&#252;&#169;&#167;")]
	[InlineData("José 😀", "&#74;&#111;&#115;&#233;&#32;&#128512;")]
	public void String_ReturnsHtmlEntityEncodedValue(string input, string expected)
	{
		// Arrange

		// Act
		var result = input.ToHtmlEntities();

		// Assert
		Assert.Equal(expected, result);
	}
}

// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Extensions;

namespace Jeebs.StringExtensions_Tests;

public class HtmlDecode_Tests
{
	[Theory]
	[InlineData("&amp;&lt;&gt;&#39;&#169;&copy;", "&<>'©©")]
	[InlineData("&lt;p&gt;Paragraph Text&lt;/p&gt;", "<p>Paragraph Text</p>")]
	public void Html_ReturnsDecodedHtml(string input, string expected)
	{
		// Arrange

		// Act
		var result = input.HtmlDecode();

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData("&<>'©©", "&amp;&lt;&gt;&#39;&#169;&#169;")]
	[InlineData("<p>Paragraph Text</p>", "&lt;p&gt;Paragraph Text&lt;/p&gt;")]
	public void Html_ReturnsEncodedHtml(string input, string expected)
	{
		// Arrange

		// Act
		var result = input.HtmlEncode();

		// Assert
		Assert.Equal(expected, result);
	}
}

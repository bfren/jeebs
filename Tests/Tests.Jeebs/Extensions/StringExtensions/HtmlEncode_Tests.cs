using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public partial class StringExtensions_Tests
	{
		[Theory]
		[InlineData("&amp;&lt;&gt;&#39;&#169;&copy;", "&<>'©©")]
		[InlineData("&lt;p&gt;Paragraph Text&lt;/p&gt;", "<p>Paragraph Text</p>")]
		public void HtmlDecode_Html_ReturnsDecodedHtml(string input, string expected)
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
		public void HtmlEncode_Html_ReturnsEncodedHtml(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.HtmlEncode();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

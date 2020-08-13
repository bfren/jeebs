using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public partial class StringExtensions_Tests
	{
		[Theory]
		[InlineData("<a href=\"test\">'Ben'</a>", "<a href=\"test\">&lsquo;Ben&rsquo;</a>")]
		[InlineData("<a href=\"test\">'Ben'</a> 'Green'", "<a href=\"test\">&lsquo;Ben&rsquo;</a> &lsquo;Green&rsquo;")]
		[InlineData("<a href=\"test\">'Ben's Test'</a>", "<a href=\"test\">&lsquo;Ben&rsquo;s Test&rsquo;</a>")]
		[InlineData("<a href=\"test\">\"Ben\"</a>", "<a href=\"test\">&ldquo;Ben&rdquo;</a>")]
		[InlineData("<a href=\"test\">\"Ben\"</a> \"Green\"", "<a href=\"test\">&ldquo;Ben&rdquo;</a> &ldquo;Green&rdquo;")]
		[InlineData("<a href=\"test\">\"Ben's Test\"</a>", "<a href=\"test\">&ldquo;Ben&rsquo;s Test&rdquo;</a>")]
		public void ConvertInnerHtmlQuotes_Html_ReturnsHtmlWithConvertedQuotes(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.ConvertInnerHtmlQuotes();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

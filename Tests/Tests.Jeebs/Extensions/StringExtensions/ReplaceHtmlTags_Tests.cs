using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs
{
	public partial class StringExtensions_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void ReplaceHtmlTags_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.ReplaceHtmlTags();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("<p>Ben</p>", "Ben")]
		[InlineData("<p class=\"attr\">Ben</p>", "Ben")]
		[InlineData("<p class=\"attr\">Ben <strong>Green</strong></p>", "Ben Green")]
		public void ReplaceHtmlTags_String_ReturnsValueWithHtmlTagsReplaced(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.ReplaceHtmlTags();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

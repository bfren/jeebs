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
		public void ConvertQuotes_NullOrEmpty_ReturnsOriginal(string input)
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
		public void ConvertQuotes_String_ReturnsValueWithQuotesConverted(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.ConvertCurlyQuotes();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

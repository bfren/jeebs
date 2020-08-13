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
		public void EscapeSingleQuotes_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.EscapeSingleQuotes();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("'", "\\'")]
		[InlineData("'Ben'", "\\'Ben\\'")]
		public void EscapeSingleQuotes_String_ReturnsValueWithSingleQuotesEscaped(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.EscapeSingleQuotes();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

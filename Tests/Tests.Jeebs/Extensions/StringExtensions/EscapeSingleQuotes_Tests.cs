// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.StringExtensions_Tests
{
	public class EscapeSingleQuotes_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void NullOrEmpty_ReturnsOriginal(string input)
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
		public void String_ReturnsValueWithSingleQuotesEscaped(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.EscapeSingleQuotes();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

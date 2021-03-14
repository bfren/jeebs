// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.StringExtensions_Tests
{
	public class EndWith_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var resultChar = input.EndWith('.');
			var resultStr = input.EndWith(".");

			// Assert
			Assert.Equal(input, resultChar);
			Assert.Equal(input, resultStr);
		}

		[Theory]
		[InlineData("Be", 'n', "Ben")]
		[InlineData("Ben", 'n', "Ben")]
		public void String_ReturnsValueEndingWithCharacter(string input, char endWith, string expected)
		{
			// Arrange

			// Act
			var result = input.EndWith(endWith);

			// Assert
			Assert.Equal(expected, result);
		}

		[Theory]
		[InlineData("Be", "n Green", "Ben Green")]
		[InlineData("Ben Green", "n Green", "Ben Green")]
		public void String_ReturnsValueEndingWithString(string input, string endWith, string expected)
		{
			// Arrange

			// Act
			var result = input.EndWith(endWith);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

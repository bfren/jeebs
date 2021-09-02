// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.StringExtensions_Tests
{
	public class SplitByCapitals_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.SplitByCapitals();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("BenjaminCharlesGreen", "Benjamin Charles Green")]
		[InlineData(" ben JaminCharlesGreen ", "ben Jamin Charles Green")]
		public void String_ReturnsValueSplitByCapitals(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.SplitByCapitals();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

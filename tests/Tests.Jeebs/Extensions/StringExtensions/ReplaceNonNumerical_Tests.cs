// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.StringExtensions_Tests
{
	public class ReplaceNonNumerical_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.ReplaceNonNumerical();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("Bro65ken12", null, "6512")]
		[InlineData("Bro65ken12", "-", "-65-12")]
		public void String_ReturnsValueWithNumbersReplaced(string input, string with, string expected)
		{
			// Arrange

			// Act
			var result = input.ReplaceNonNumerical(with);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

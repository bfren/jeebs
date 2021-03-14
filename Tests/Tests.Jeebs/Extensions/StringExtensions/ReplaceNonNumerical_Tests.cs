// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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

// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.StringExtensions_Tests
{
	public class ToUpperFirst_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.ToUpperFirst();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("bEN", "BEN")]
		[InlineData("Ben", "Ben")]
		public void String_ReturnsValueWithUppercaseFirstLetter(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.ToUpperFirst();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

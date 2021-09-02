// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.StringExtensions_Tests
{
	public class StartWith_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.StartWith(default);

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("en", 'B', "Ben")]
		[InlineData("Ben", 'B', "Ben")]
		[InlineData("ben", 'B', "Bben")]
		public void String_ReturnsValueStartingWithCharacter(string input, char startWith, string expected)
		{
			// Arrange

			// Act
			var result = input.StartWith(startWith);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

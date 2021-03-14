// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.StringExtensions_Tests
{
	public class NullIfEmpty_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		public void NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.NullIfEmpty();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("Ben")]
		public void String_ReturnsOriginalValue(string input)
		{
			// Arrange

			// Act
			var result = input.NullIfEmpty();

			// Assert
			Assert.Equal(input, result);
		}
	}
}

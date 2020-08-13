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
		public void NoLongerThan_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.NoLongerThan(10);

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData(null, 0, "..", "Empty", "Empty")]
		[InlineData("123", 4, null, null, "123")]
		[InlineData("1234", 4, null, null, "1234")]
		[InlineData("12345", 4, "..", null, "1234..")]
		[InlineData("12345", 4, null, null, "1234")]
		public void NoLongerThan_String_ReturnsTruncatedValue(string input, int max, string continuation, string empty, string expected)
		{
			// Arrange

			// Act
			var result = input.NoLongerThan(max, continuation, empty);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

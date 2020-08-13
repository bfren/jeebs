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
		public void ReplaceNonNumerical_NullOrEmpty_ReturnsOriginal(string input)
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
		public void ReplaceNonNumerical_String_ReturnsValueWithNumbersReplaced(string input, string with, string expected)
		{
			// Arrange

			// Act
			var result = input.ReplaceNonNumerical(with);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

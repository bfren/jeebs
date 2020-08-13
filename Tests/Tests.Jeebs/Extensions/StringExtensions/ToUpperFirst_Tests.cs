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
		public void ToUpperFirst_NullOrEmpty_ReturnsOriginal(string input)
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
		public void ToUpperFirst_String_ReturnsValueWithUppercaseFirstLetter(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.ToUpperFirst();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

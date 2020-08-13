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
		public void ToTitleCase_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.ToTitleCase();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("this is a test sentence", "This Is A Test Sentence")]
		[InlineData("testing The PHP acronym", "Testing The PHP Acronym")]
		public void ToTitleCase_String_ReturnsValueInTitleCase(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.ToTitleCase();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

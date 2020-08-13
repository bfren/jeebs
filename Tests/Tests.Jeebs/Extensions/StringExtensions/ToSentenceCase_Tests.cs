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
		public void ToSentenceCase_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.ToSentenceCase();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("this is a test sentence", "This is a test sentence")]
		[InlineData("testing The PHP acronym", "Testing the php acronym")]
		public void ToSentenceCase_String_ReturnsValueInSentenceCase(string input, string expected)
		{
			// Arrange

			// Act
			var result = input.ToSentenceCase();

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

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
		public void StartWith_NullOrEmpty_ReturnsOriginal(string input)
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
		public void StartWith_String_ReturnsValueStartingWithCharacter(string input, char startWith, string expected)
		{
			// Arrange

			// Act
			var result = input.StartWith(startWith);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

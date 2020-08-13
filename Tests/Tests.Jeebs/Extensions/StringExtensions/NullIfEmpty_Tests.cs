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
		public void NullIfEmpty_NullOrEmpty_ReturnsOriginal(string input)
		{
			// Arrange

			// Act
			var result = input.NullIfEmpty();

			// Assert
			Assert.Equal(input, result);
		}

		[Theory]
		[InlineData("Ben")]
		public void NullIfEmpty_String_ReturnsOriginalValue(string input)
		{
			// Arrange

			// Act
			var result = input.NullIfEmpty();

			// Assert
			Assert.Equal(input, result);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace F
{
	public sealed class BooleanFTests
	{
		[Theory]
		[InlineData("true")]
		[InlineData("TRUE")]
		[InlineData("true,false")]
		[InlineData("TRUE,FALSE")]
		[InlineData("on")]
		[InlineData("ON")]
		[InlineData("yes")]
		[InlineData("YES")]
		[InlineData("1")]
		public void TryParse_String_ReturnsTrue(string input)
		{
			// Arrange

			// Act
			var result = BooleanF.Parse(input);

			// Assert
			Assert.IsType<Some<bool>>(result);
		}

		[Theory]
		[InlineData("false")]
		[InlineData("FALSE")]
		[InlineData("off")]
		[InlineData("OFF")]
		[InlineData("no")]
		[InlineData("NO")]
		[InlineData("0")]
		public void TryParse_String_ReturnsFalse(string input)
		{
			// Arrange

			// Act
			var result = BooleanF.Parse(input);

			// Assert
			Assert.IsType<Some<bool>>(result);
		}

		[Fact]
		public void TryParse_InvalidString_ReturnsFalse()
		{
			// Arrange
			const string input = "this is not a valid boolean";

			// Act
			var result = BooleanF.Parse(input);

			// Assert
			Assert.IsType<None<bool>>(result);
		}
	}
}

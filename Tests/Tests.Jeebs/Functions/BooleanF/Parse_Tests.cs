using System;
using System.Collections.Generic;
using System.Text;
using Jeebs;
using Xunit;

namespace F
{
	public sealed class BooleanF_Tests
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
		public void Parse_String_Returns_Some_True(string input)
		{
			// Arrange

			// Act
			var result = BooleanF.Parse(input);

			// Assert
			var some = Assert.IsType<Some<bool>>(result);
			Assert.True(some.Value);
		}

		[Theory]
		[InlineData("false")]
		[InlineData("FALSE")]
		[InlineData("off")]
		[InlineData("OFF")]
		[InlineData("no")]
		[InlineData("NO")]
		[InlineData("0")]
		public void Parse_String_Returns_Some_False(string input)
		{
			// Arrange

			// Act
			var result = BooleanF.Parse(input);

			// Assert
			var some = Assert.IsType<Some<bool>>(result);
			Assert.False(some.Value);
		}

		[Theory]
		[InlineData("2")]
		[InlineData("this is not a valid boolean")]
		public void TryParse_InvalidString_Returns_None(string input)
		{
			// Arrange

			// Act
			var result = BooleanF.Parse(input);

			// Assert
			Assert.IsType<None<bool>>(result);
		}
	}
}

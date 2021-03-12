// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs;
using static F.BooleanF;
using Xunit;
using F.BooleanFMsg;

namespace F.BooleanF_Tests
{
	public class Parse_Tests
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
		public void String_Returns_Some_True(string input)
		{
			// Arrange

			// Act
			var result = Parse(input);

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
		public void String_Returns_Some_False(string input)
		{
			// Arrange

			// Act
			var result = Parse(input);

			// Assert
			var some = Assert.IsType<Some<bool>>(result);
			Assert.False(some.Value);
		}

		[Theory]
		[InlineData("2")]
		[InlineData("this is not a valid boolean")]
		public void InvalidString_Returns_None_With_UnrecognisedValueMsg(string input)
		{
			// Arrange

			// Act
			var result = Parse(input);

			// Assert
			var none = Assert.IsType<None<bool>>(result);
			Assert.IsType<UnrecognisedValueMsg>(none.Reason);
		}
	}
}

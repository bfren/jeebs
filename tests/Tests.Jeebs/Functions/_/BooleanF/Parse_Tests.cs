// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions.BooleanF_Tests;

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
		var result = BooleanF.Parse(input);

		// Assert
		var some = result.AssertSome();
		Assert.True(some);
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
		var result = BooleanF.Parse(input);

		// Assert
		var some = result.AssertSome();
		Assert.False(some);
	}

	[Theory]
	[InlineData("2")]
	[InlineData("this is not a valid boolean")]
	public void InvalidString_Returns_None_With_UnrecognisedValueMsg(string input)
	{
		// Arrange

		// Act
		var result = BooleanF.Parse(input);

		// Assert
		result.AssertNone();
	}
}

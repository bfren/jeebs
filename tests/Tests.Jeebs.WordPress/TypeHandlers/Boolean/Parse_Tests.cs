// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.WordPress.Data.TypeHandlers.BooleanTypeHandler_Tests;

public class Parse_Tests
{
	[Theory]
	[InlineData(1)]
	[InlineData("1")]
	[InlineData("y")]
	[InlineData("Y")]
	[InlineData("yes")]
	public void Valid_Value_Returns_True(object input)
	{
		// Arrange
		var handler = new BooleanTypeHandler();

		// Act
		var result = handler.Parse(input);

		// Assert
		Assert.True(result);
	}

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(0)]
	[InlineData("0")]
	[InlineData(2)]
	[InlineData("2")]
	[InlineData(true)]
	[InlineData("true")]
	[InlineData(false)]
	[InlineData("false")]
	[InlineData("n")]
	[InlineData("N")]
	[InlineData("no")]
	public void Invalid_Value_Returns_False(object input)
	{
		// Arrange
		var handler = new BooleanTypeHandler();

		// Act
		var result = handler.Parse(input);

		// Assert
		Assert.False(result);
	}
}

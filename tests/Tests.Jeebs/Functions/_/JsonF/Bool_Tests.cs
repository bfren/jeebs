// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions.JsonF_Tests;

public class Bool_Tests
{
	[Theory]
	[InlineData(true, "true")]
	[InlineData(false, "false")]
	public void Returns_Correct_Value(bool value, string expected)
	{
		// Arrange

		// Act
		var result = JsonF.Bool(value);

		// Assert
		Assert.Equal(expected, result);
	}
}

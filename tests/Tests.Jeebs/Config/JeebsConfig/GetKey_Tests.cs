// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.JeebsConfig_Tests;

public class GetKey_Tests
{
	[Theory]
	[InlineData("one:two:three", "one:two:three")]
	[InlineData(":four", "jeebs:four")]
	public void Key_ReturnsNormalOrJeebsKey(string input, string expected)
	{
		// Arrange

		// Act
		var result = JeebsConfig.GetKey(input);

		// Assert
		Assert.Equal(expected, result);
	}
}

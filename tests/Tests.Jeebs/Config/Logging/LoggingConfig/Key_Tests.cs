// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.Logging.LoggingConfig_Tests;

public class Key_Tests
{
	[Fact]
	public void Returns_Logging_Key()
	{
		// Arrange

		// Act
		var result = LoggingConfig.Key;

		// Assert
		Assert.Equal(JeebsConfig.Key + ":logging", result);
	}
}

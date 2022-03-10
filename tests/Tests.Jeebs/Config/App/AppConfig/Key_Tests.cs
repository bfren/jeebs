// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.App.AppConfig_Tests;

public class Key_Tests
{
	[Fact]
	public void Returns_App_Key()
	{
		// Arrange

		// Act
		var result = AppConfig.Key;

		// Assert
		Assert.Equal(JeebsConfig.Key + ":app", result);
	}
}

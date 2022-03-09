﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.Web.WebConfig_Tests;

public class Key_Tests
{
	[Fact]
	public void Returns_Web_Key()
	{
		// Arrange

		// Act
		var result = WebConfig.Key;

		// Assert
		Assert.Equal(JeebsConfig.Key + ":web", result);
	}
}

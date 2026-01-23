// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.Web.Redirections.RedirectionsConfig_Tests;

public class Key_Tests
{
	[Fact]
	public void Returns_Redirections_Key()
	{
		// Arrange

		// Act
		var result = RedirectionsConfig.Key;

		// Assert
		Assert.Equal(JeebsConfig.Key + ":web:redirections", result);
	}
}

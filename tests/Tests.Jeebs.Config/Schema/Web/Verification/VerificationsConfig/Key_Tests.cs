// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Config.VerificationsConfig_Tests;

public class Key_Tests
{
	[Fact]
	public void Returns_Web_Key()
	{
		// Arrange

		// Act
		const string result = VerificationConfig.Key;

		// Assert
		Assert.Equal(JeebsConfig.Key + ":web:verification", result);
	}
}

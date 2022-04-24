// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.Azure.DataProtection.DataProtectionConfig_Tests;

public class Key_Tests
{
	[Fact]
	public void Returns_AzureDataProtection_Key()
	{
		// Arrange

		// Act
		var result = DataProtectionConfig.Key;

		// Assert
		Assert.Equal(AzureConfig.Key + ":dataProtection", result);
	}
}

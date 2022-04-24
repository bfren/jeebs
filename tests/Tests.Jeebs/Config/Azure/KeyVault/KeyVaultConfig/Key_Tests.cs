// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.Azure.KeyVault.KeyVaultConfig_Tests;

public class Key_Tests
{
	[Fact]
	public void Returns_AzureKeyVault_Key()
	{
		// Arrange

		// Act
		var result = KeyVaultConfig.Key;

		// Assert
		Assert.Equal(AzureConfig.Key + ":keyVault", result);
	}
}

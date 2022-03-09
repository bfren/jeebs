// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.AzureKeyVault.AzureKeyVault_Tests;

public class Key_Tests
{
	[Fact]
	public void Returns_AzureKeyVault_Key()
	{
		// Arrange

		// Act
		var result = AzureKeyVaultConfig.Key;

		// Assert
		Assert.Equal(JeebsConfig.Key + ":azureKeyVault", result);
	}
}

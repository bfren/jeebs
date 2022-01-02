// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Config.AzureKeyVault_Tests;

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

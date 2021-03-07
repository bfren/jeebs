// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Config.AzureKeyVault_Tests
{
	public class Key_Tests
	{
		[Fact]
		public void Returns_AzureKeyVault_Key()
		{
			// Arrange

			// Act
			const string result = AzureKeyVaultConfig.Key;

			// Assert
			Assert.Equal(JeebsConfig.Key + ":azureKeyVault", result);
		}
	}
}

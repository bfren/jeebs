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

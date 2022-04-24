// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.Azure.KeyVault.KeyVaultConfig_Tests;

public class GetUri_Tests
{
	[Fact]
	public void Returns_Uri__With_Correct_Value()
	{
		// Arrange
		var name = Rnd.Str;
		var config = new KeyVaultConfig { Name = name };

		// Act
		var result = config.GetUri();

		// Assert
		Assert.Equal($"https://{name.ToLowerInvariant()}.vault.azure.net/", result.AbsoluteUri);
	}
}

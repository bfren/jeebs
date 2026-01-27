// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.Azure.KeyVault.KeyVaultConfig_Tests;

public class IsValid_Tests
{
	[Theory]
	[InlineData(null, "tenant", "id", "secret")]
	[InlineData("", "tenant", "id", "secret")]
	[InlineData(" ", "tenant", "id", "secret")]
	[InlineData("name", null, "id", "secret")]
	[InlineData("name", "", "id", "secret")]
	[InlineData("name", " ", "id", "secret")]
	[InlineData("name", "tenant", null, "secret")]
	[InlineData("name", "tenant", "", "secret")]
	[InlineData("name", "tenant", " ", "secret")]
	[InlineData("name", "tenant", "id", null)]
	[InlineData("name", "tenant", "id", "")]
	[InlineData("name", "tenant", "id", " ")]
	public void Returns_False(string? name, string? tenantId, string? clientId, string? clientSecret)
	{
		// Arrange
		var config = new KeyVaultConfig
		{
			Name = name!,
			TenantId = tenantId!,
			ClientId = clientId!,
			ClientSecret = clientSecret!
		};

		// Act
		var result = config.IsValid;

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Returns_True()
	{
		// Arrange
		var config = new KeyVaultConfig
		{
			Name = Rnd.Str,
			TenantId = Rnd.Str,
			ClientId = Rnd.Str,
			ClientSecret = Rnd.Str
		};

		// Act
		var result = config.IsValid;

		// Assert
		Assert.True(result);
	}
}

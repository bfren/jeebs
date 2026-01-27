// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Cryptography.Functions.CryptoF_Tests;

public sealed class GenerateKey_Tests
{
	[Fact]
	public void GenerateKey_ReturnsUniqueValues()
	{
		// Arrange

		// Act
		var r0 = CryptoF.GenerateKey();
		var r1 = CryptoF.GenerateKey();

		// Assert
		var ok0 = r0.AssertOk();
		var ok1 = r1.AssertOk();
		Assert.NotEqual(ok0, ok1);
	}

	[Fact]
	public void GenerateKey_Returns32ByteArray()
	{
		// Arrange

		// Act
		var result = CryptoF.GenerateKey();

		// Assert
		var ok = result.AssertOk();
		Assert.Equal(32, ok.Length);
	}
}

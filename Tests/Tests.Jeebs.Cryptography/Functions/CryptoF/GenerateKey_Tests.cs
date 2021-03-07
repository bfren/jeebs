// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace F.CryptoF_Tests
{
	public sealed class GenerateKey_Tests
	{
		[Fact]
		public void GenerateKey_ReturnsUniqueValues()
		{
			// Arrange

			// Act
			var key1 = CryptoF.GenerateKey();
			var key2 = CryptoF.GenerateKey();

			// Assert
			Assert.NotEqual(key1, key2);
		}

		[Fact]
		public void GenerateKey_Returns32ByteArray()
		{
			// Arrange

			// Act
			var key = CryptoF.GenerateKey();

			// Assert
			Assert.Equal(32, key.Length);
		}
	}
}

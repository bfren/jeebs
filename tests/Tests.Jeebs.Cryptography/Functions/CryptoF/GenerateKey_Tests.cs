// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs;
using Xunit;
using static F.CryptoF;

namespace F.CryptoF_Tests
{
	public sealed class GenerateKey_Tests
	{
		[Fact]
		public void GenerateKey_ReturnsUniqueValues()
		{
			// Arrange

			// Act
			var r0 = GenerateKey();
			var r1 = GenerateKey();

			// Assert
			var s0 = r0.AssertSome();
			var s1 = r1.AssertSome();
			Assert.NotEqual(s0, s1);
		}

		[Fact]
		public void GenerateKey_Returns32ByteArray()
		{
			// Arrange

			// Act
			var result = GenerateKey();

			// Assert
			var some = result.AssertSome();
			Assert.Equal(32, some.Length);
		}
	}
}

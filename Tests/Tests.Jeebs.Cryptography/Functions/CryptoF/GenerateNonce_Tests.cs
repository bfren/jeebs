using Xunit;

namespace F.CryptoF_Tests
{
	public class GenerateNonce_Tests
	{
		[Fact]
		public void GenerateNonce_ReturnsUniqueValues()
		{
			// Arrange

			// Act
			var nonce1 = CryptoF.GenerateNonce();
			var nonce2 = CryptoF.GenerateNonce();

			// Assert
			Assert.NotEqual(nonce1, nonce2);
		}

		[Fact]
		public void GenerateNonce_Returns24ByteArray()
		{
			// Arrange

			// Act
			var nonce = CryptoF.GenerateNonce();

			// Assert
			Assert.Equal(24, nonce.Length);
		}
	}
}

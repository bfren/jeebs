using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Config.AzureKeyVault_Tests
{
	public class IsValid_Tests
	{
		[Theory]
		[InlineData(null, "id", "secret")]
		[InlineData("", "id", "secret")]
		[InlineData(" ", "id", "secret")]
		[InlineData("name", null, "secret")]
		[InlineData("name", "", "secret")]
		[InlineData("name", " ", "secret")]
		[InlineData("name", "id", null)]
		[InlineData("name", "id", "")]
		[InlineData("name", "id", " ")]
		public void Returns_False(string name, string clientId, string clientSecret)
		{
			// Arrange
			var config = new AzureKeyVaultConfig
			{
				Name = name,
				ClientId = clientId,
				ClientSecret = clientSecret
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
			var config = new AzureKeyVaultConfig
			{
				Name = F.Rnd.String,
				ClientId = F.Rnd.String,
				ClientSecret = F.Rnd.String
			};

			// Act
			var result = config.IsValid;

			// Assert
			Assert.True(result);
		}
	}
}

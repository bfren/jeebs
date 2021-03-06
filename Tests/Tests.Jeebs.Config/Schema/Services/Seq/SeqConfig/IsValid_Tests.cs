using Xunit;

namespace Jeebs.Config.SeqConfig_Tests
{
	public class IsValid_Tests
	{
		[Theory]
		[InlineData(null, "api")]
		[InlineData("", "api")]
		[InlineData(" ", "api")]
		[InlineData("server", null)]
		[InlineData("server", "")]
		[InlineData("server", " ")]
		public void Returns_False(string server, string apiKey)
		{
			// Arrange
			var config = new SeqConfig
			{
				Server = server,
				ApiKey = apiKey,
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
			var config = new SeqConfig
			{
				Server = "https://news.contoso.com",
				ApiKey = F.Rnd.Str
			};

			// Act
			var result = config.IsValid;

			// Assert
			Assert.True(result);
		}
	}
}

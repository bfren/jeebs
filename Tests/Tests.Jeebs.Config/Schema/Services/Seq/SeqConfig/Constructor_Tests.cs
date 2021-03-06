using Xunit;

namespace Jeebs.Config.SeqConfig_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Implements_ServiceConfig()
		{
			// Arrange
			var config = new SeqConfig();

			// Act

			// Assert
			Assert.IsAssignableFrom<ServiceConfig>(config);
		}

		[Fact]
		public void Implements_WebhookServiceConfig()
		{
			// Arrange
			var config = new SeqConfig();

			// Act

			// Assert
			Assert.IsAssignableFrom<WebhookServiceConfig>(config);
		}
	}
}

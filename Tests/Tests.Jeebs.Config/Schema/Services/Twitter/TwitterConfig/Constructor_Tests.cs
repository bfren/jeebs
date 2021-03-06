using Xunit;

namespace Jeebs.Config.TwitterConfig_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Implements_ServiceConfig()
		{
			// Arrange
			var config = new TwitterConfig();

			// Act

			// Assert
			Assert.IsAssignableFrom<ServiceConfig>(config);
		}
	}
}

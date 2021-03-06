using Xunit;

namespace Jeebs.Config.WebConfig_Tests
{
	public class Key_Tests
	{
		[Fact]
		public void Returns_Web_Key()
		{
			// Arrange

			// Act
			const string result = WebConfig.Key;

			// Assert
			Assert.Equal(JeebsConfig.Key + ":web", result);
		}
	}
}

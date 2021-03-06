using Xunit;

namespace Jeebs.Config.WpConfig_Tests
{
	public class Key_Tests
	{
		[Fact]
		public void Returns_Web_Key()
		{
			// Arrange

			// Act
			const string result = WpConfig.Key;

			// Assert
			Assert.Equal(JeebsConfig.Key + ":wp", result);
		}
	}
}

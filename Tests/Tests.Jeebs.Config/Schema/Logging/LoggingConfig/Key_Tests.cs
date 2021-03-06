using Xunit;

namespace Jeebs.Config.LoggingConfig_Tests
{
	public class Key_Tests
	{
		[Fact]
		public void Returns_Logging_Key()
		{
			// Arrange

			// Act
			const string result = LoggingConfig.Key;

			// Assert
			Assert.Equal(JeebsConfig.Key + ":logging", result);
		}
	}
}

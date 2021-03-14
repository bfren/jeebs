// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Config.AppConfig_Tests
{
	public class Key_Tests
	{
		[Fact]
		public void Returns_App_Key()
		{
			// Arrange

			// Act
			const string result = AppConfig.Key;

			// Assert
			Assert.Equal(JeebsConfig.Key + ":app", result);
		}
	}
}

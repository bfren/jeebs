// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Config.VerificationsConfig_Tests
{
	public class Key_Tests
	{
		[Fact]
		public void Returns_Web_Key()
		{
			// Arrange

			// Act
			const string result = VerificationConfig.Key;

			// Assert
			Assert.Equal(JeebsConfig.Key + ":web:verification", result);
		}
	}
}

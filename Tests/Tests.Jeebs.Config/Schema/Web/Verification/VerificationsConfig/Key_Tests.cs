using System;
using System.Collections.Generic;
using System.Text;
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

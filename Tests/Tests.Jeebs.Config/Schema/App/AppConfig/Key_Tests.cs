using System;
using System.Collections.Generic;
using System.Text;
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

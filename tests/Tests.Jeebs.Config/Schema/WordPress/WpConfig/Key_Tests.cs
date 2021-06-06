// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

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

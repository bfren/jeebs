using System;
using Xunit;

namespace Jeebs.Config
{
	public sealed class JeebsConfig_Tests
	{
		[Theory]
		[InlineData("one:two:three", "one:two:three")]
		[InlineData(":four", "jeebs:four")]
		public void GetSettingsKey_Key_ReturnsNormalOrJeebsKey(string input, string expected)
		{
			// Arrange

			// Act
			var result = JeebsConfig.GetKey(input);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

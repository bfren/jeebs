// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Config.RocketChatConfig_Tests
{
	public class IsValid_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		[InlineData("http://news.contoso.com")]
		public void Returns_False(string webhook)
		{
			// Arrange
			var config = new RocketChatConfig { Webhook = webhook };

			// Act
			var result = config.IsValid;

			// Assert
			Assert.False(result);
		}

		[Theory]
		[InlineData("https://news.contoso.com")]
		public void Returns_True(string webhook)
		{
			// Arrange
			var config = new RocketChatConfig { Webhook = webhook };

			// Act
			var result = config.IsValid;

			// Assert
			Assert.True(result);
		}
	}
}

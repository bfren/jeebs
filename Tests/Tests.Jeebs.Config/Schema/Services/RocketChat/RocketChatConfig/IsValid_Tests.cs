using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Jeebs.Config.RocketChatConfig_Tests
{
	public class IsValid_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void Returns_False(string webhook)
		{
			// Arrange
			var config = new RocketChatConfig { Webhook = webhook };

			// Act
			var result = config.IsValid;

			// Assert
			Assert.False(result);
		}

		[Fact]
		public void Returns_True()
		{
			// Arrange
			var config = new RocketChatConfig { Webhook = F.Rnd.String };

			// Act
			var result = config.IsValid;

			// Assert
			Assert.True(result);
		}
	}
}

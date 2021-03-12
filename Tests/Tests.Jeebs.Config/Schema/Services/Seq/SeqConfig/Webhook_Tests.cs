// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Config.SeqConfig_Tests
{
	public class Webhook_Tests
	{
		[Fact]
		public void Set_Does_Nothing()
		{
			// Arrange
			var config = new SeqConfig
			{
				Webhook = JeebsF.Rnd.Str
			};

			// Act
			var result = config.Webhook;

			// Assert
			Assert.Equal("/api/events/raw?clef", result);
		}

		[Fact]
		public void Returns_With_Server_Value()
		{
			// Arrange
			var server = JeebsF.Rnd.Str;
			var config = new SeqConfig { Server = server };

			// Act
			var result = config.Webhook;

			// Assert
			Assert.Equal($"{server}/api/events/raw?clef", result);
		}
	}
}

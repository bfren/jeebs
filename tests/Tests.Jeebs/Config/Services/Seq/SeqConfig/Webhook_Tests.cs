// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.Services.Seq.SeqConfig_Tests;

public class Webhook_Tests
{
	[Fact]
	public void Returns_With_Server_Value()
	{
		// Arrange
		var server = Rnd.Str;
		var config = new SeqConfig { Server = server };

		// Act
		var result = config.Webhook;

		// Assert
		Assert.Equal($"{server}/api/events/raw?clef", result);
	}
}

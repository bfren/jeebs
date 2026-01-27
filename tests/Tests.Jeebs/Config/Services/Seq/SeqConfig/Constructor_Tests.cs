// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.Services.Seq.SeqConfig_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Implements_ServiceConfig()
	{
		// Arrange
		var config = new SeqConfig();

		// Act

		// Assert
		Assert.IsType<IServiceConfig>(config, exactMatch: false);
	}

	[Fact]
	public void Implements_IWebhookServiceConfig()
	{
		// Arrange
		var config = new SeqConfig();

		// Act

		// Assert
		Assert.IsType<IWebhookServiceConfig>(config, exactMatch: false);
	}
}

// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Config.SeqConfig_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Implements_ServiceConfig()
	{
		// Arrange
		var config = new SeqConfig();

		// Act

		// Assert
		Assert.IsAssignableFrom<IServiceConfig>(config);
	}

	[Fact]
	public void Implements_IWebhookServiceConfig()
	{
		// Arrange
		var config = new SeqConfig();

		// Act

		// Assert
		Assert.IsAssignableFrom<IWebhookServiceConfig>(config);
	}
}

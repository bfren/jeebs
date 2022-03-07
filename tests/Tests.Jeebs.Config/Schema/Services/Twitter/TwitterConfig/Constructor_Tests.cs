// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Config.TwitterConfig_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Implements_ServiceConfig()
	{
		// Arrange
		var config = new TwitterConfig();

		// Act

		// Assert
		_ = Assert.IsAssignableFrom<IServiceConfig>(config);
	}
}

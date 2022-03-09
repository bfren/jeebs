// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.App.AppConfig_Tests;

public class Version_Tests
{
	[Fact]
	public void Default_Version_Returns_0100()
	{
		// Arrange
		var config = new AppConfig();

		// Act
		var result = config.Version;

		// Assert
		Assert.Equal(0, result.Major);
		Assert.Equal(1, result.Minor);
		Assert.Equal(0, result.Build);
		Assert.Equal(0, result.Revision);
	}
}

// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions.VersionF_Tests;

public class GetVersionString_Tests
{
	[Fact]
	public void Returns_Formatted_String()
	{
		// Arrange
		var version = Setup.NewVersion;

		// Act
		var result = VersionF.GetVersionString(version);

		// Assert
		Assert.Equal($"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}", result);
	}
}

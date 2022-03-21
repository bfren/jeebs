// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions.VersionF_Tests;

public class AssemblyVersion_Tests
{
	[Fact]
	public void Does_Not_Return_Empty_Version()
	{
		// Arrange

		// Act
		var result = VersionF.AssemblyVersion;

		// Assert
		var version = Assert.IsType<Version>(result);
		Assert.NotEqual(new(), version);
	}
}

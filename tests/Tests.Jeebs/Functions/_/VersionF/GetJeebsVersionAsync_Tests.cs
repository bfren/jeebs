// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Functions.VersionF_Tests;

public class GetJeebsVersionAsync_Tests
{
	[Fact]
	public async Task Returns_Correct_Version()
	{
		// Arrange
		var expected = await Setup.JeebsVersion.Value;

		// Act
		var result = await VersionF.GetJeebsVersionAsync();

		// Assert
		Assert.Equal(expected, result);
	}
}

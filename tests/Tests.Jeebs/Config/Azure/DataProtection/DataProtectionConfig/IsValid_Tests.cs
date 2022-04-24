// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Config.Azure.DataProtection.DataProtectionConfig_Tests;

public class IsValid_Tests
{
	[Theory]
	[InlineData(null, "container", "blob", "key")]
	[InlineData("", "container", "blob", "key")]
	[InlineData(" ", "container", "blob", "key")]
	[InlineData("cs", null, "blob", "key")]
	[InlineData("cs", "", "blob", "key")]
	[InlineData("cs", " ", "blob", "key")]
	[InlineData("cs", "container", null, "key")]
	[InlineData("cs", "container", "", "key")]
	[InlineData("cs", "container", " ", "key")]
	[InlineData("cs", "container", "blob", null)]
	[InlineData("cs", "container", "blob", "")]
	[InlineData("cs", "container", "blob", " ")]
	public void Returns_False(string cs, string container, string blob, string key)
	{
		// Arrange
		var config = new DataProtectionConfig
		{
			StorageAccessKeyConnectionString = cs,
			ContainerName = container,
			BlobName = blob,
			KeyUri = key
		};

		// Act
		var result = config.IsValid;

		// Assert
		Assert.False(result);
	}

	[Fact]
	public void Returns_True()
	{
		// Arrange
		var config = new DataProtectionConfig
		{
			StorageAccessKeyConnectionString = Rnd.Str,
			ContainerName = Rnd.Str,
			BlobName = Rnd.Str,
			KeyUri = Rnd.Str
		};

		// Act
		var result = config.IsValid;

		// Assert
		Assert.True(result);
	}
}

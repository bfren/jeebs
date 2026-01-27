// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Text;

namespace Jeebs.Functions.VersionF_Tests;

public class GetVersion_Tests
{
	[Fact]
	public async Task Stream_Not_Null_Returns_Stream_Value()
	{
		// Arrange
		var value = Rnd.Str;
		var stream = new MemoryStream(Encoding.UTF8.GetBytes(value));

		// Act
		var result = await VersionF.GetVersion(stream, null);

		// Assert
		Assert.Equal(value, result);
	}

	[Fact]
	public async Task Stream_Not_Null_Catches_Stream_Exception_Returns_Version()
	{
		// Arrange
		var stream = new MemoryStream();
		stream.Dispose(); // causes StreamReader.ReadToEndAsync() to throw an exception
		var version = Setup.NewVersion;
		var expected = VersionF.GetVersionString(version);

		// Act
		var result = await VersionF.GetVersion(stream, version);

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public async Task Stream_Null_Returns_Version()
	{
		// Arrange
		var version = Setup.NewVersion;
		var expected = VersionF.GetVersionString(version);

		// Act
		var result = await VersionF.GetVersion(null, version);

		// Assert
		Assert.Equal(expected, result);
	}

	[Fact]
	public async Task Stream_Null_And_Version_Null_Returns_Empty()
	{
		// Arrange

		// Act
		var result = await VersionF.GetVersion(null, null);

		// Assert
		Assert.Equal("0.0.0.0", result);
	}
}

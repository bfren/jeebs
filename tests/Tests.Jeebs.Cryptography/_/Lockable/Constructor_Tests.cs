// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Cryptography.Lockable_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_Properties()
	{
		// Arrange
		var value = Rnd.Str;
		var box = new Lockable<string>(value);

		// Act
		var result = box.Contents;

		// Assert
		Assert.Equal(value, result);
	}
}

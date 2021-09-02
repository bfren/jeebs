// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Internals;
using Xunit;

namespace Jeebs.Some_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_Value()
	{
		// Arrange
		var value = F.Rnd.Str;

		// Act
		var result = new Some<string>(value);

		// Assert
		Assert.Equal(value, result.Value);
	}
}

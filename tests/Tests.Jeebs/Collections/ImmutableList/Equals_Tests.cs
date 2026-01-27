// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Collections.ImmutableList_Tests;

public class Equals_Tests
{
	[Fact]
	public void Lists_Equal_Returns_True()
	{
		// Arrange
		var i0 = Rnd.Str;
		var i1 = Rnd.Str;
		var i2 = Rnd.Str;
		var l0 = new ImmutableList<string>(new[] { i0, i1, i2 });
		var l1 = new ImmutableList<string>(new[] { i0, i1, i2 });

		// Act
		var result = l0 == l1;

		// Assert
		Assert.True(result);
	}

	[Fact]
	public void Lists_Not_Equal_Returns_False()
	{
		// Arrange
		var i0 = Rnd.Str;
		var i1 = Rnd.Str;
		var i2 = Rnd.Str;
		var l0 = new ImmutableList<string>(new[] { i0, i1, i2 });
		var l1 = new ImmutableList<string>(new[] { i2, i1, i0 });

		// Act
		var result = l0 == l1;

		// Assert
		Assert.False(result);
	}
}

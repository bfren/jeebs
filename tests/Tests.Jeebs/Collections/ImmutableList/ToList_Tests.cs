// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Collections.ImmutableList_Tests;

public class ToList_Tests
{
	[Fact]
	public void Returns_List()
	{
		// Arrange
		var i0 = Rnd.Int;
		var i1 = Rnd.Int;
		var list = new ImmutableList<int>(new[] { i0, i1 });

		// Act
		var result = list.ToList();

		// Assert
		_ = Assert.IsType<List<int>>(result);
		Assert.Collection(result,
			x => Assert.Equal(i0, x),
			x => Assert.Equal(i1, x)
		);
	}

	[Fact]
	public void Returns_Copy()
	{
		// Arrange
		var i0 = Rnd.Str;
		var i1 = Rnd.Str;
		var list = new ImmutableList<string>(new[] { i0, i1 });

		// Act
		var copy = list.ToList();
		i0 = Rnd.Str;
		i1 = Rnd.Str;

		// Assert
		Assert.Collection(copy,
			x => Assert.NotEqual(i0, x),
			x => Assert.NotEqual(i1, x)
		);
	}
}

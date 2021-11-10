// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.ImmutableList_Tests;

public class ToArray_Tests
{
	[Fact]
	public void Returns_Array()
	{
		// Arrange
		var i0 = F.Rnd.Int;
		var i1 = F.Rnd.Int;
		var list = new ImmutableList<int>(new[] { i0, i1 });

		// Act
		var result = list.ToArray();

		// Assert
		Assert.IsType<int[]>(result);
		Assert.Collection(result,
			x => Assert.Equal(i0, x),
			x => Assert.Equal(i1, x)
		);
	}

	[Fact]
	public void Returns_Copy()
	{
		// Arrange
		var i0 = F.Rnd.Str;
		var i1 = F.Rnd.Str;
		var list = new ImmutableList<string>(new[] { i0, i1 });

		// Act
		var copy = list.ToArray();
		i0 = F.Rnd.Str;
		i1 = F.Rnd.Str;

		// Assert
		Assert.Collection(copy,
			x => Assert.NotEqual(i0, x),
			x => Assert.NotEqual(i1, x)
		);
	}
}

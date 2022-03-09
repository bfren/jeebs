// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Collections.ImmutableList_Tests;

public class WithoutRange_Tests
{
	[Fact]
	public void Returns_List_With_Items_Removed()
	{
		// Arrange
		var i0 = Rnd.Str;
		var i1 = Rnd.Str;
		var i2 = Rnd.Str;
		var i3 = Rnd.Str;
		var list = new ImmutableList<string>(new[] { i0, i1, i2, i3 });

		// Act
		var result = list.WithoutRange(new[] { i2, i3 });

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(i0, x),
			x => Assert.Equal(i1, x)
		);
	}

	[Fact]
	public void Returns_New_List_With_Items_Removed()
	{
		// Arrange
		var i0 = Rnd.Str;
		var i1 = Rnd.Str;
		var i2 = Rnd.Str;
		var i3 = Rnd.Str;
		var list = new ImmutableList<string>(new[] { i0, i1, i2, i3 });

		// Act
		var result = list.WithoutRange(new[] { i2, i3 });
		i0 = Rnd.Str;
		i1 = Rnd.Str;

		// Assert
		Assert.Collection(result,
			x => Assert.NotEqual(i0, x),
			x => Assert.NotEqual(i1, x)
		);
	}
}

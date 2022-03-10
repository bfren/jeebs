﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Collections.ImmutableList_Tests;

public class Replace_Tests
{
	[Fact]
	public void Returns_List_With_Item_Replaced()
	{
		// Arrange
		var i0 = Rnd.Str;
		var i1 = Rnd.Str;
		var i2 = Rnd.Str;
		var list = new ImmutableList<string>(new[] { i0, i1 });

		// Act
		var result = list.Replace(i0, i2);

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(i2, x),
			x => Assert.Equal(i1, x)
		);
	}

	[Fact]
	public void Returns_New_List_With_Item_Replaced()
	{
		// Arrange
		var i0 = Rnd.Str;
		var i1 = Rnd.Str;
		var i2 = Rnd.Str;
		var list = new ImmutableList<string>(new[] { i0, i1 });

		// Act
		var result = list.Replace(i0, i2);
		i1 = Rnd.Str;
		i2 = Rnd.Str;

		// Assert
		Assert.Collection(result,
			x => Assert.NotEqual(i2, x),
			x => Assert.NotEqual(i1, x)
		);
	}
}

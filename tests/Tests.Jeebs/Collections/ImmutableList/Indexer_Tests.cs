// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Functions;

namespace Jeebs.Collections.ImmutableList_Tests;

public class Indexer_Tests
{
	[Fact]
	public void Exists_Returns_Some_With_Value()
	{
		// Arrange
		var i0 = Rnd.Str;
		var i1 = Rnd.Str;
		var i2 = Rnd.Str;
		var list = ListF.Create([i0, i1, i2]);

		// Act
		var result = list[1];

		// Assert
		var some = result.AssertSome();
		Assert.Equal(i1, some);
	}

	[Fact]
	public void Does_Not_Exist_Returns_None()
	{
		// Arrange
		var i0 = Rnd.Str;
		var i1 = Rnd.Str;
		var list = ListF.Create([i0, i1]);

		// Act
		var result = list[2];

		// Assert
		result.AssertNone();
	}

	[Fact]
	public void Empty_List_Returns_None()
	{
		// Arrange
		var list = ListF.Create<int>();

		// Act
		var result = list[Rnd.Int];

		// Assert
		result.AssertNone();
	}

	[Fact]
	public void Element_Is_Read_Only()
	{
		// Arrange
		var i0 = Rnd.Str;
		var i1 = Rnd.Str;
		var list = ListF.Create([i0, i1]);

		// Act
		for (var i = 0; i < list.Count; i++)
		{
			var item = list[i];
			item = Rnd.Str;
		}

		// Assert
		Assert.Collection(list,
			x => Assert.Equal(i0, x),
			x => Assert.Equal(i1, x)
		);
	}
}

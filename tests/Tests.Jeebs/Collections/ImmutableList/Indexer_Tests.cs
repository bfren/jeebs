// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using static MaybeF.F.EnumerableF.R;

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
		var list = ImmutableList.Create(new[] { i0, i1, i2 });

		// Act
		var result = list[1];

		// Assert
		var some = result.AssertSome();
		Assert.Equal(i1, some);
	}

	[Fact]
	public void Does_Not_Exist_Returns_None_With_ElementAtIsNullMsg()
	{
		// Arrange
		var i0 = Rnd.Str;
		var i1 = Rnd.Str;
		var list = ImmutableList.Create(new[] { i0, i1 });

		// Act
		var result = list[2];

		// Assert
		result.AssertNone().AssertType<ElementAtIsNullReason>();
	}

	[Fact]
	public void Empty_List_Returns_None_With_ListIsEmptyMsg()
	{
		// Arrange
		var list = new ImmutableList<int>();

		// Act
		var result = list[Rnd.Int];

		// Assert
		result.AssertNone().AssertType<ListIsEmptyReason>();
	}

	[Fact]
	public void Element_Is_Read_Only()
	{
		// Arrange
		var i0 = Rnd.Str;
		var i1 = Rnd.Str;
		var list = ImmutableList.Create(new[] { i0, i1 });

		// Act
		for (int i = 0; i < list.Count; i++)
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

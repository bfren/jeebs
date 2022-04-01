// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using static Jeebs.Collections.ListExtensions.M;

namespace Jeebs.Collections.ListExtensions_Tests;

public class GetEitherSide_Tests
{
	[Fact]
	public void Empty_List_Returns_None_With_ListIsEmptyMsg()
	{
		// Arrange
		var list = new List<int>();

		// Act
		var (r0, r1) = list.GetEitherSide(Rnd.Int);

		// Assert
		r0.AssertNone().AssertType<ListIsEmptyMsg>();
		r1.AssertNone().AssertType<ListIsEmptyMsg>();
	}

	[Fact]
	public void Single_Item_Returns_None_With_ListContainsSingleItemMsg()
	{
		// Arrange
		var list = new List<int> { Rnd.Int };

		// Act
		var (r0, r1) = list.GetEitherSide(Rnd.Int);

		// Assert
		r0.AssertNone().AssertType<ListContainsSingleItemMsg>();
		r1.AssertNone().AssertType<ListContainsSingleItemMsg>();
	}

	[Fact]
	public void Item_Not_In_List_Returns_None_With_ListDoesNotContainItemMsg()
	{
		// Arrange
		var value = -1;
		var list = new List<int> { Rnd.Int, Rnd.Int, Rnd.Int };

		// Act
		var (r0, r1) = list.GetEitherSide(value);

		// Assert
		var n0 = r0.AssertNone().AssertType<ListDoesNotContainItemMsg<int>>();
		Assert.Equal(value, n0.Value);
		var n1 = r1.AssertNone().AssertType<ListDoesNotContainItemMsg<int>>();
		Assert.Equal(value, n1.Value);
	}

	[Fact]
	public void First_Item_Returns_None_And_Next_Item()
	{
		// Arrange
		var value = Rnd.Int;
		var next = Rnd.Int;
		var list = new List<int> { value, next, Rnd.Int, Rnd.Int };

		// Act
		var (r0, r1) = list.GetEitherSide(value);

		// Assert
		r0.AssertNone().AssertType<ItemIsFirstItemMsg>();
		var some = r1.AssertSome();
		Assert.Equal(next, some);
	}

	[Fact]
	public void Last_Item_Returns_Previous_Item_And_None()
	{
		// Arrange
		var value = Rnd.Int;
		var prev = Rnd.Int;
		var list = new List<int> { Rnd.Int, Rnd.Int, prev, value };

		// Act
		var (r0, r1) = list.GetEitherSide(value);

		// Assert
		var some = r0.AssertSome();
		Assert.Equal(prev, some);
		r1.AssertNone().AssertType<ItemIsLastItemMsg>();
	}

	[Fact]
	public void Returns_Previous_And_Next_Items()
	{
		// Arrange
		var value = Rnd.Int;
		var prev = Rnd.Int;
		var next = Rnd.Int;
		var list = new List<int> { Rnd.Int, Rnd.Int, prev, value, next, Rnd.Int, Rnd.Int };

		// Act
		var (r0, r1) = list.GetEitherSide(value);

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(prev, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(next, s1);
	}
}

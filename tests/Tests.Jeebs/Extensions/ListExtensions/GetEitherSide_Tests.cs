// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static Jeebs.ListExtensions.Msg;

namespace Jeebs.ListExtensions_Tests;

public class GetEitherSide_Tests
{
	[Fact]
	public void Empty_List_Returns_None_With_ListIsEmptyMsg()
	{
		// Arrange
		var list = new List<int>();

		// Act
		var (r0, r1) = list.GetEitherSide(F.Rnd.Int);

		// Assert
		var n0 = r0.AssertNone();
		Assert.IsType<ListIsEmptyMsg>(n0);
		var n1 = r1.AssertNone();
		Assert.IsType<ListIsEmptyMsg>(n1);
	}

	[Fact]
	public void Single_Item_Returns_None_With_ListContainsSingleItemMsg()
	{
		// Arrange
		var list = new List<int> { F.Rnd.Int };

		// Act
		var (r0, r1) = list.GetEitherSide(F.Rnd.Int);

		// Assert
		var n0 = r0.AssertNone();
		Assert.IsType<ListContainsSingleItemMsg>(n0);
		var n1 = r1.AssertNone();
		Assert.IsType<ListContainsSingleItemMsg>(n1);
	}

	[Fact]
	public void Item_Not_In_List_Returns_None_With_ListDoesNotContainItemMsg()
	{
		// Arrange
		var value = -1;
		var list = new List<int> { F.Rnd.Int, F.Rnd.Int, F.Rnd.Int };

		// Act
		var (r0, r1) = list.GetEitherSide(value);

		// Assert
		var n0 = r0.AssertNone();
		var m0 = Assert.IsType<ListDoesNotContainItemMsg<int>>(n0);
		Assert.Equal(value, m0.Value);
		var n1 = r1.AssertNone();
		var m1 = Assert.IsType<ListDoesNotContainItemMsg<int>>(n1);
		Assert.Equal(value, m1.Value);
	}

	[Fact]
	public void First_Item_Returns_None_And_Next_Item()
	{
		// Arrange
		var value = F.Rnd.Int;
		var next = F.Rnd.Int;
		var list = new List<int> { value, next, F.Rnd.Int, F.Rnd.Int };

		// Act
		var (r0, r1) = list.GetEitherSide(value);

		// Assert
		var none = r0.AssertNone();
		Assert.IsType<ItemIsFirstItemMsg>(none);
		var some = r1.AssertSome();
		Assert.Equal(next, some);
	}

	[Fact]
	public void Last_Item_Returns_Previous_Item_And_None()
	{
		// Arrange
		var value = F.Rnd.Int;
		var prev = F.Rnd.Int;
		var list = new List<int> { F.Rnd.Int, F.Rnd.Int, prev, value };

		// Act
		var (r0, r1) = list.GetEitherSide(value);

		// Assert
		var some = r0.AssertSome();
		Assert.Equal(prev, some);
		var none = r1.AssertNone();
		Assert.IsType<ItemIsLastItemMsg>(none);
	}

	[Fact]
	public void Returns_Previous_And_Next_Items()
	{
		// Arrange
		var value = F.Rnd.Int;
		var prev = F.Rnd.Int;
		var next = F.Rnd.Int;
		var list = new List<int> { F.Rnd.Int, F.Rnd.Int, prev, value, next, F.Rnd.Int, F.Rnd.Int };

		// Act
		var (r0, r1) = list.GetEitherSide(value);

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(prev, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(next, s1);
	}
}

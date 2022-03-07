// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.MaybeF.Enumerable.M;

namespace Jeebs_Tests.Enumerable;

public abstract class FirstOrNone_Tests
{
	public abstract void Test00_Empty_List_Returns_None_With_ListIsEmptyMsg();

	protected static void Test00(Func<IEnumerable<int>, Maybe<int>> act)
	{
		// Arrange
		var list = Array.Empty<int>();

		// Act
		var result = act(list);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<ListIsEmptyMsg>(none);
	}

	public abstract void Test01_No_Matching_Items_Returns_None_With_FirstItemIsNullMsg();

	protected static void Test01(Func<IEnumerable<int?>, Func<int?, bool>, Maybe<int?>> act)
	{
		// Arrange
		var list = new int?[] { F.Rnd.Int, F.Rnd.Int, F.Rnd.Int };
		var predicate = Substitute.For<Func<int?, bool>>();
		predicate.Invoke(Arg.Any<int?>()).Returns(false);

		// Act
		var result = act(list, predicate);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<FirstItemIsNullMsg>(none);
	}

	public abstract void Test02_Returns_First_Element();

	protected static void Test02(Func<IEnumerable<int>, Maybe<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var list = new[] { value, F.Rnd.Int, F.Rnd.Int };

		// Act
		var result = act(list);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}

	public abstract void Test03_Returns_First_Matching_Element();

	protected static void Test03(Func<IEnumerable<int>, Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var list = new[] { F.Rnd.Int, value, F.Rnd.Int };
		var predicate = Substitute.For<Func<int, bool>>();
		predicate.Invoke(value).Returns(true);

		// Act
		var result = act(list, predicate);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}
}

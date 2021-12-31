// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Collections.Generic;
using Jeebs;
using Xunit;
using static F.OptionF.Enumerable.M;

namespace Jeebs_Tests.Enumerable;

public abstract class ElementAtOrNone_Tests
{
	public abstract void Test00_Empty_List_Returns_None_With_ListIsEmptyMsg();

	protected static void Test00(Func<IEnumerable<int>, int, Option<int>> act)
	{
		// Arrange
		var list = Array.Empty<int>();

		// Act
		var result = act(list, 0);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<ListIsEmptyMsg>(none);
	}

	public abstract void Test01_No_Value_At_Index_Returns_None_With_ElementAtIsNullMsg();

	protected static void Test01(Func<IEnumerable<int?>, int, Option<int?>> act)
	{
		// Arrange
		var list = new int?[] { F.Rnd.Int, F.Rnd.Int, F.Rnd.Int };

		// Act
		var result = act(list, 4);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<ElementAtIsNullMsg>(none);
	}

	public abstract void Test02_Value_At_Index_Returns_Some_With_Value();

	protected static void Test02(Func<IEnumerable<int>, int, Option<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var list = new[] { F.Rnd.Int, value, F.Rnd.Int };

		// Act
		var result = act(list, 1);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}
}

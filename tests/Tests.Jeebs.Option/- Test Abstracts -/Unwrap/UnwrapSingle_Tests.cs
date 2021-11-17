// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.M;

namespace Jeebs_Tests;

public abstract class UnwrapSingle_Tests
{
	public abstract void Test00_If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg();

	protected static void Test00(Func<Option<int>, Option<int>> act)
	{
		// Arrange
		var option = new FakeOption();

		// Act
		var result = act(option);

		// Assert
		var none = result.AssertNone();
		var msg = Assert.IsType<UnhandledExceptionMsg>(none);
		Assert.IsType<UnknownOptionException>(msg.Value);
	}

	public abstract void Test01_None_Returns_None();

	protected static void Test01(Func<Option<int>, Option<int>> act)
	{
		// Arrange
		var option = Create.None<int>();

		// Act
		var result = act(option);

		// Assert
		result.AssertNone();
	}

	public abstract void Test02_None_With_Reason_Returns_None_With_Reason();

	protected static void Test02(Func<Option<int>, Option<int>> act)
	{
		// Arrange
		var reason = new TestMsg();
		var option = None<int>(reason);

		// Act
		var result = act(option);

		// Assert
		var none = result.AssertNone();
		Assert.Same(reason, none);
	}

	public abstract void Test03_No_Items_Returns_None_With_UnwrapSingleNoItemsMsg();

	protected static void Test03(Func<Option<int[]>, Option<int>> act)
	{
		// Arrange
		var empty = Array.Empty<int>();
		var option = Some(empty);

		// Act
		var result = act(option);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<UnwrapSingleNoItemsMsg>(none);
	}

	public abstract void Test04_No_Items_Runs_NoItems();

	protected static void Test04(Func<Option<int[]>, Func<Msg>?, Option<int>> act)
	{
		// Arrange
		var empty = Array.Empty<int>();
		var option = Some(empty);
		var noItems = Substitute.For<Func<Msg>>();

		// Act
		act(option, noItems);

		// Assert
		noItems.Received().Invoke();
	}

	public abstract void Test05_Too_Many_Items_Returns_None_With_UnwrapSingleTooManyItemsErrorMsg();

	protected static void Test05(Func<Option<int[]>, Option<int>> act)
	{
		// Arrange
		var list = new[] { F.Rnd.Int, F.Rnd.Int };
		var option = Some(list);

		// Act
		var result = act(option);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<UnwrapSingleTooManyItemsErrorMsg>(none);
	}

	public abstract void Test06_Too_Many_Items_Runs_TooMany();

	protected static void Test06(Func<Option<int[]>, Func<Msg>?, Option<int>> act)
	{
		// Arrange
		var list = new[] { F.Rnd.Int, F.Rnd.Int };
		var option = Some(list);
		var tooMany = Substitute.For<Func<Msg>>();

		// Act
		act(option, tooMany);

		// Assert
		tooMany.Received().Invoke();
	}

	public abstract void Test07_Not_A_List_Returns_None_With_UnwrapSingleNotAListMsg();

	protected static void Test07(Func<Option<int>, Option<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var option = Some(value);

		// Act
		var result = act(option);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<UnwrapSingleNotAListMsg>(none);
	}

	public abstract void Test08_Not_A_List_Runs_NotAList();

	protected static void Test08(Func<Option<int>, Func<Msg>?, Option<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var option = Some(value);
		var notAList = Substitute.For<Func<Msg>>();

		// Act
		var result = act(option, notAList);

		// Assert
		notAList.Received().Invoke();
	}

	public abstract void Test09_Incorrect_Type_Returns_None_With_UnwrapSingleIncorrectTypeErrorMsg();

	protected static void Test09(Func<Option<int[]>, Option<string>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var list = new[] { value };
		var option = Some(list);

		// Act
		var result = act(option);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<UnwrapSingleIncorrectTypeErrorMsg>(none);
	}

	public abstract void Test10_List_With_Single_Item_Returns_Single();

	protected static void Test10(Func<Option<int[]>, Option<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var list = new[] { value };
		var option = Some(list);

		// Act
		var result = act(option);

		// Assert
		Assert.Equal(value, result);
	}

	public record class FakeOption : Option<int> { }

	public record class TestMsg : Msg;
}

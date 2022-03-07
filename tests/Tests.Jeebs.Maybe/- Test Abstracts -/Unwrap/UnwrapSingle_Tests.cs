// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.MaybeF;
using static F.MaybeF.M;

namespace Jeebs_Tests;

public abstract class UnwrapSingle_Tests
{
	public abstract void Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionMsg();

	protected static void Test00(Func<Maybe<int>, Maybe<int>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var result = act(maybe);

		// Assert
		var none = result.AssertNone();
		var msg = Assert.IsType<UnhandledExceptionMsg>(none);
		_ = Assert.IsType<UnknownMaybeException>(msg.Value);
	}

	public abstract void Test01_None_Returns_None();

	protected static void Test01(Func<Maybe<int>, Maybe<int>> act)
	{
		// Arrange
		var maybe = Create.None<int>();

		// Act
		var result = act(maybe);

		// Assert
		_ = result.AssertNone();
	}

	public abstract void Test02_None_With_Reason_Returns_None_With_Reason();

	protected static void Test02(Func<Maybe<int>, Maybe<int>> act)
	{
		// Arrange
		var reason = new TestMsg();
		var maybe = None<int>(reason);

		// Act
		var result = act(maybe);

		// Assert
		var none = result.AssertNone();
		Assert.Same(reason, none);
	}

	public abstract void Test03_No_Items_Returns_None_With_UnwrapSingleNoItemsMsg();

	protected static void Test03(Func<Maybe<int[]>, Maybe<int>> act)
	{
		// Arrange
		var empty = Array.Empty<int>();
		var maybe = Some(empty);

		// Act
		var result = act(maybe);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<UnwrapSingleNoItemsMsg>(none);
	}

	public abstract void Test04_No_Items_Runs_NoItems();

	protected static void Test04(Func<Maybe<int[]>, Func<Msg>?, Maybe<int>> act)
	{
		// Arrange
		var empty = Array.Empty<int>();
		var maybe = Some(empty);
		var noItems = Substitute.For<Func<Msg>>();

		// Act
		_ = act(maybe, noItems);

		// Assert
		_ = noItems.Received().Invoke();
	}

	public abstract void Test05_Too_Many_Items_Returns_None_With_UnwrapSingleTooManyItemsErrorMsg();

	protected static void Test05(Func<Maybe<int[]>, Maybe<int>> act)
	{
		// Arrange
		var list = new[] { F.Rnd.Int, F.Rnd.Int };
		var maybe = Some(list);

		// Act
		var result = act(maybe);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<UnwrapSingleTooManyItemsErrorMsg>(none);
	}

	public abstract void Test06_Too_Many_Items_Runs_TooMany();

	protected static void Test06(Func<Maybe<int[]>, Func<Msg>?, Maybe<int>> act)
	{
		// Arrange
		var list = new[] { F.Rnd.Int, F.Rnd.Int };
		var maybe = Some(list);
		var tooMany = Substitute.For<Func<Msg>>();

		// Act
		_ = act(maybe, tooMany);

		// Assert
		_ = tooMany.Received().Invoke();
	}

	public abstract void Test07_Not_A_List_Returns_None_With_UnwrapSingleNotAListMsg();

	protected static void Test07(Func<Maybe<int>, Maybe<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Some(value);

		// Act
		var result = act(maybe);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<UnwrapSingleNotAListMsg>(none);
	}

	public abstract void Test08_Not_A_List_Runs_NotAList();

	protected static void Test08(Func<Maybe<int>, Func<Msg>?, Maybe<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Some(value);
		var notAList = Substitute.For<Func<Msg>>();

		// Act
		_ = act(maybe, notAList);

		// Assert
		_ = notAList.Received().Invoke();
	}

	public abstract void Test09_Incorrect_Type_Returns_None_With_UnwrapSingleIncorrectTypeErrorMsg();

	protected static void Test09(Func<Maybe<int[]>, Maybe<string>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var list = new[] { value };
		var maybe = Some(list);

		// Act
		var result = act(maybe);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<UnwrapSingleIncorrectTypeErrorMsg>(none);
	}

	public abstract void Test10_List_With_Single_Item_Returns_Single();

	protected static void Test10(Func<Maybe<int[]>, Maybe<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var list = new[] { value };
		var maybe = Some(list);

		// Act
		var result = act(maybe);

		// Assert
		Assert.Equal(value, result);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg : Msg;
}

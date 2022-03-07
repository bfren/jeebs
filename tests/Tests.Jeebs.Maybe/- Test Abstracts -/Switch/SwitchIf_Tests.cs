// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using Jeebs.Exceptions;
using Jeebs.Internals;
using NSubstitute;
using Xunit;
using static F.MaybeF;
using static F.MaybeF.M;

namespace Jeebs_Tests;

public abstract class SwitchIf_Tests
{
	public abstract void Test00_Unknown_Maybe_Throws_UnknownOptionException();

	protected static void Test00(Func<Maybe<int>, Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();
		var check = Substitute.For<Func<int, bool>>();

		// Act
		var action = void () => act(maybe, check);

		// Assert
		_ = Assert.Throws<UnknownMaybeException>(action);
	}

	public abstract void Test01_None_Returns_Original_None();

	protected static void Test01(Func<Maybe<int>, Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var maybe = Create.None<int>();
		var check = Substitute.For<Func<int, bool>>();

		// Act
		var result = act(maybe, check);

		// Assert
		_ = result.AssertNone();
		Assert.Same(maybe, result);
	}

	public abstract void Test02_Check_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg();

	protected static void Test02(Func<Maybe<int>, Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var maybe = Some(F.Rnd.Int);
		var check = bool (int _) => throw new MaybeTestException();

		// Act
		var result = act(maybe, check);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<SwitchIfFuncExceptionMsg>(none);
	}

	public abstract void Test03_Check_Returns_True_And_IfTrue_Is_Null_Returns_Original_Option();

	protected static void Test03(Func<Maybe<int>, Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var maybe = Some(F.Rnd.Int);
		var check = Substitute.For<Func<int, bool>>();
		_ = check.Invoke(Arg.Any<int>()).Returns(true);

		// Act
		var result = act(maybe, check);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract void Test04_Check_Returns_False_And_IfFalse_Is_Null_Returns_Original_Option();

	protected static void Test04(Func<Maybe<int>, Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var maybe = Some(F.Rnd.Int);
		var check = Substitute.For<Func<int, bool>>();
		_ = check.Invoke(Arg.Any<int>()).Returns(false);

		// Act
		var result = act(maybe, check);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract void Test05_Check_Returns_True_And_IfTrue_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg();

	protected static void Test05(Func<Maybe<int>, Func<int, bool>, Func<int, None<int>>, Maybe<int>> act)
	{
		// Arrange
		var maybe = Some(F.Rnd.Int);
		var check = Substitute.For<Func<int, bool>>();
		_ = check.Invoke(Arg.Any<int>()).Returns(true);
		var ifTrue = None<int> (int _) => throw new MaybeTestException();

		// Act
		var result = act(maybe, check, ifTrue);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<SwitchIfFuncExceptionMsg>(none);
	}

	public abstract void Test06_Check_Returns_False_And_IfFalse_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg();

	protected static void Test06(Func<Maybe<int>, Func<int, bool>, Func<int, None<int>>, Maybe<int>> act)
	{
		// Arrange
		var maybe = Some(F.Rnd.Int);
		var check = Substitute.For<Func<int, bool>>();
		_ = check.Invoke(Arg.Any<int>()).Returns(false);
		var ifFalse = None<int> (int _) => throw new MaybeTestException();

		// Act
		var result = act(maybe, check, ifFalse);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<SwitchIfFuncExceptionMsg>(none);
	}

	public abstract void Test07_Check_Returns_True_Runs_IfTrue_Returns_Value();

	protected static void Test07(Func<Maybe<int>, Func<int, bool>, Func<int, Maybe<int>>, Maybe<int>> act)
	{
		// Arrange
		var v0 = F.Rnd.Int;
		var v1 = F.Rnd.Int;
		var maybe = Some(v0);
		var check = Substitute.For<Func<int, bool>>();
		_ = check.Invoke(v0).Returns(true);
		var ifTrue = Substitute.For<Func<int, Maybe<int>>>();
		_ = ifTrue.Invoke(v0).Returns(Some(v0 + v1));

		// Act
		var result = act(maybe, check, ifTrue);

		// Assert
		_ = ifTrue.Received().Invoke(v0);
		var some = result.AssertSome();
		Assert.Equal(v0 + v1, some);
	}

	public abstract void Test08_Check_Returns_False_Runs_IfFalse_Returns_Value();

	protected static void Test08(Func<Maybe<int>, Func<int, bool>, Func<int, None<int>>, Maybe<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Some(value);
		var check = Substitute.For<Func<int, bool>>();
		_ = check.Invoke(value).Returns(false);
		var ifFalse = Substitute.For<Func<int, None<int>>>();
		_ = ifFalse.Invoke(value).Returns(None<int, TestMsg>());

		// Act
		var result = act(maybe, check, ifFalse);

		// Assert
		_ = ifFalse.Received().Invoke(value);
		var none = result.AssertNone();
		_ = Assert.IsType<TestMsg>(none);
	}

	public record class FakeMaybe : Maybe<int> { }

	public sealed record class TestMsg : Msg;
}

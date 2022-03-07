// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.MaybeF;

namespace Jeebs_Tests;

public abstract class Audit_Tests
{
	#region General

	public abstract void Test00_Null_Args_Returns_Original_Option();

	protected static void Test00(Func<Maybe<int>, Maybe<int>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var result = act(maybe);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract void Test01_If_Unknown_Maybe_Throws_UnknownOptionException();

	protected static void Test01(Func<Maybe<int>, Maybe<int>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		void result() => act(maybe);

		// Assert
		_ = Assert.Throws<UnknownMaybeException>(result);
	}

	#endregion General

	#region Any

	public abstract void Test02_Some_Runs_Audit_And_Returns_Original_Option();

	protected static void Test02(Func<Maybe<bool>, Action<Maybe<bool>>, Maybe<bool>> act)
	{
		// Arrange
		var maybe = True;
		var audit = Substitute.For<Action<Maybe<bool>>>();

		// Act
		var result = act(maybe, audit);

		// Assert
		audit.Received().Invoke(maybe);
		Assert.Same(maybe, result);
	}

	public abstract void Test03_None_Runs_Audit_And_Returns_Original_Option();

	protected static void Test03(Func<Maybe<bool>, Action<Maybe<bool>>, Maybe<bool>> act)
	{
		// Arrange
		var maybe = Create.None<bool>();
		var audit = Substitute.For<Action<Maybe<bool>>>();

		// Act
		var result = act(maybe, audit);

		// Assert
		audit.Received().Invoke(maybe);
		Assert.Same(maybe, result);
	}

	public abstract void Test04_Some_Catches_Exception_And_Returns_Original_Option();

	protected static void Test04(Func<Maybe<bool>, Action<Maybe<bool>>, Maybe<bool>> act)
	{
		// Arrange
		var some = True;
		var throwException = void (Maybe<bool> _) => throw new MaybeTestException();

		// Act
		var result = act(some, throwException);

		// Assert
		Assert.Same(some, result);
	}

	public abstract void Test05_None_Catches_Exception_And_Returns_Original_Option();

	protected static void Test05(Func<Maybe<bool>, Action<Maybe<bool>>, Maybe<bool>> act)
	{
		// Arrange
		var none = Create.None<bool>();
		var throwException = void (Maybe<bool> _) => throw new MaybeTestException();

		// Act
		var result = act(none, throwException);

		// Assert
		Assert.Same(none, result);
	}

	#endregion Any

	#region Some / None

	public abstract void Test06_Some_Runs_Some_And_Returns_Original_Option();

	protected static void Test06(Func<Maybe<int>, Action<int>, Maybe<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Some(value);
		var some = Substitute.For<Action<int>>();

		// Act
		var result = act(maybe, some);

		// Assert
		some.Received().Invoke(value);
		Assert.Same(maybe, result);
	}

	public abstract void Test07_None_Runs_None_And_Returns_Original_Option();

	protected static void Test07(Func<Maybe<int>, Action<Msg>, Maybe<int>> act)
	{
		// Arrange
		var msg = new TestMsg();
		var maybe = None<int>(msg);
		var none = Substitute.For<Action<Msg>>();

		// Act
		var result = act(maybe, none);

		// Assert
		none.Received().Invoke(msg);
		Assert.Same(maybe, result);
	}

	public abstract void Test08_Some_Catches_Exception_And_Returns_Original_Option();

	protected static void Test08(Func<Maybe<int>, Action<int>, Maybe<int>> act)
	{
		// Arrange
		var maybe = Some(F.Rnd.Int);
		var throwException = void (int _) => throw new MaybeTestException();

		// Act
		var result = act(maybe, throwException);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract void Test09_None_Catches_Exception_And_Returns_Original_Option();

	protected static void Test09(Func<Maybe<int>, Action<Msg>, Maybe<int>> act)
	{
		// Arrange
		var maybe = Create.None<int>();
		var exception = new Exception();
		var throwException = void (Msg _) => throw exception;

		// Act
		var result = act(maybe, throwException);

		// Assert
		Assert.Same(maybe, result);
	}

	#endregion Some / None

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg : Msg;
}

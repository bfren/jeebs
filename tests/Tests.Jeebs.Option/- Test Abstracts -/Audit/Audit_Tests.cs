// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs_Tests;

public abstract class Audit_Tests
{
	#region General

	public abstract void Test00_Null_Args_Returns_Original_Option();

	protected static void Test00(Func<Option<int>, Option<int>> act)
	{
		// Arrange
		var option = new FakeOption();

		// Act
		var result = act(option);

		// Assert
		Assert.Same(option, result);
	}

	public abstract void Test01_If_Unknown_Option_Throws_UnknownOptionException();

	protected static void Test01(Func<Option<int>, Option<int>> act)
	{
		// Arrange
		var option = new FakeOption();

		// Act
		void result() => act(option);

		// Assert
		Assert.Throws<UnknownOptionException>(result);
	}

	#endregion

	#region Any

	public abstract void Test02_Some_Runs_Audit_And_Returns_Original_Option();

	protected static void Test02(Func<Option<bool>, Action<Option<bool>>, Option<bool>> act)
	{
		// Arrange
		var option = True;
		var audit = Substitute.For<Action<Option<bool>>>();

		// Act
		var result = act(option, audit);

		// Assert
		audit.Received().Invoke(option);
		Assert.Same(option, result);
	}

	public abstract void Test03_None_Runs_Audit_And_Returns_Original_Option();

	protected static void Test03(Func<Option<bool>, Action<Option<bool>>, Option<bool>> act)
	{
		// Arrange
		var option = Create.None<bool>();
		var audit = Substitute.For<Action<Option<bool>>>();

		// Act
		var result = act(option, audit);

		// Assert
		audit.Received().Invoke(option);
		Assert.Same(option, result);
	}

	public abstract void Test04_Some_Catches_Exception_And_Returns_Original_Option();

	protected static void Test04(Func<Option<bool>, Action<Option<bool>>, Option<bool>> act)
	{
		// Arrange
		var some = True;
		var throwException = void (Option<bool> _) => throw new Exception("Thrown.");

		// Act
		var result = act(some, throwException);

		// Assert
		Assert.Same(some, result);
	}

	public abstract void Test05_None_Catches_Exception_And_Returns_Original_Option();

	protected static void Test05(Func<Option<bool>, Action<Option<bool>>, Option<bool>> act)
	{
		// Arrange
		var none = Create.None<bool>();
		var throwException = void (Option<bool> _) => throw new Exception("Thrown.");

		// Act
		var result = act(none, throwException);

		// Assert
		Assert.Same(none, result);
	}

	#endregion

	#region Some / None

	public abstract void Test06_Some_Runs_Some_And_Returns_Original_Option();

	protected static void Test06(Func<Option<int>, Action<int>, Option<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var option = Some(value);
		var some = Substitute.For<Action<int>>();

		// Act
		var result = act(option, some);

		// Assert
		some.Received().Invoke(value);
		Assert.Same(option, result);
	}

	public abstract void Test07_None_Runs_None_And_Returns_Original_Option();

	protected static void Test07(Func<Option<int>, Action<Msg>, Option<int>> act)
	{
		// Arrange
		var msg = new TestMsg();
		var option = None<int>(msg);
		var none = Substitute.For<Action<Msg>>();

		// Act
		var result = act(option, none);

		// Assert
		none.Received().Invoke(msg);
		Assert.Same(option, result);
	}

	public abstract void Test08_Some_Catches_Exception_And_Returns_Original_Option();

	protected static void Test08(Func<Option<int>, Action<int>, Option<int>> act)
	{
		// Arrange
		var option = Some(F.Rnd.Int);
		var exception = new Exception();
		var throwException = void (int _) => throw new Exception("Thrown.");

		// Act
		var result = act(option, throwException);

		// Assert
		Assert.Same(option, result);
	}

	public abstract void Test09_None_Catches_Exception_And_Returns_Original_Option();

	protected static void Test09(Func<Option<int>, Action<Msg>, Option<int>> act)
	{
		// Arrange
		var option = Create.None<int>();
		var exception = new Exception();
		var throwException = void (Msg _) => throw exception;

		// Act
		var result = act(option, throwException);

		// Assert
		Assert.Same(option, result);
	}

	#endregion

	public record class FakeOption : Option<int> { }

	public record class TestMsg : Msg;
}

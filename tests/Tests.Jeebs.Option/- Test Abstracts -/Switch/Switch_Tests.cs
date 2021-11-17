// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs_Tests;

public abstract class Switch_Tests
{
	public abstract void Test00_Return_Void_If_Unknown_Option_Throws_UnknownOptionException();

	protected static void Test00(Action<Option<int>> act)
	{
		// Arrange
		var option = new FakeOption();

		// Act
		var action = void () => act(option);

		// Assert
		Assert.Throws<UnknownOptionException>(action);
	}

	public abstract void Test01_Return_Value_If_Unknown_Option_Throws_UnknownOptionException();

	protected static void Test01(Func<Option<int>, string> act)
	{
		// Arrange
		var option = new FakeOption();

		// Act
		var action = void () => act(option);

		// Assert
		Assert.Throws<UnknownOptionException>(action);
	}

	public abstract void Test02_Return_Void_If_None_Runs_None_Action_With_Reason();

	protected static void Test02(Action<Option<int>, Action<Msg>> act)
	{
		// Arrange
		var reason = new TestMsg();
		var option = None<int>(reason);
		var none = Substitute.For<Action<Msg>>();

		// Act
		act(option, none);

		// Assert
		none.Received().Invoke(reason);
	}

	public abstract void Test03_Return_Value_If_None_Runs_None_Func_With_Reason();

	protected static void Test03(Func<Option<int>, Func<Msg, string>, string> act)
	{
		// Arrange
		var reason = new TestMsg();
		var option = None<int>(reason);
		var none = Substitute.For<Func<Msg, string>>();

		// Act
		var result = act(option, none);

		// Assert
		none.Received().Invoke(reason);
	}

	public abstract void Test04_Return_Void_If_Some_Runs_Some_Action_With_Value();

	protected static void Test04(Action<Option<int>, Action<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var option = Some(value);
		var some = Substitute.For<Action<int>>();

		// Act
		act(option, some);

		// Assert
		some.Received().Invoke(value);
	}

	public abstract void Test05_Return_Value_If_Some_Runs_Some_Func_With_Value();

	protected static void Test05(Func<Option<int>, Func<int, string>, string> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var option = Some(value);
		var some = Substitute.For<Func<int, string>>();

		// Act
		var result = act(option, some);

		// Assert
		some.Received().Invoke(value);
	}

	public record class FakeOption : Option<int> { }

	public record class TestMsg : Msg;
}

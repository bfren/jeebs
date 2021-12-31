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

public abstract class Filter_Tests
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

	public abstract void Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg();

	protected static void Test01(Func<Option<string>, Func<string, bool>, Option<string>> act)
	{
		// Arrange
		var option = Some(F.Rnd.Str);
		var exception = new Exception();
		var throwFunc = bool (string _) => throw exception;

		// Act
		var result = act(option, throwFunc);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<UnhandledExceptionMsg>(none);
	}

	public abstract void Test02_When_Some_And_Predicate_True_Returns_Value();

	protected static void Test02(Func<Option<int>, Func<int, bool>, Option<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var option = Some(value);
		var predicate = Substitute.For<Func<int, bool>>();
		predicate.Invoke(Arg.Any<int>()).Returns(true);

		// Act
		var result = act(option, predicate);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}

	public abstract void Test03_When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseMsg();

	protected static void Test03(Func<Option<string>, Func<string, bool>, Option<string>> act)
	{
		// Arrange
		var value = F.Rnd.Str;
		var option = Some(value);
		var predicate = Substitute.For<Func<string, bool>>();
		predicate.Invoke(Arg.Any<string>()).Returns(false);

		// Act
		var result = act(option, predicate);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<FilterPredicateWasFalseMsg>(none);
	}

	public abstract void Test04_When_None_Returns_None_With_Original_Reason();

	protected static void Test04(Func<Option<int>, Func<int, bool>, Option<int>> act)
	{
		// Arrange
		var reason = new TestMsg();
		var option = None<int>(reason);
		var predicate = Substitute.For<Func<int, bool>>();

		// Act
		var result = act(option, predicate);

		// Assert
		var none = result.AssertNone();
		Assert.Same(reason, none);
		predicate.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>());
	}

	public record class FakeOption : Option<int> { }

	public record class TestMsg : Msg;
}

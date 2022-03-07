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

public abstract class Filter_Tests
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

	public abstract void Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg();

	protected static void Test01(Func<Maybe<string>, Func<string, bool>, Maybe<string>> act)
	{
		// Arrange
		var maybe = Some(F.Rnd.Str);
		var exception = new Exception();
		var throwFunc = bool (string _) => throw exception;

		// Act
		var result = act(maybe, throwFunc);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<UnhandledExceptionMsg>(none);
	}

	public abstract void Test02_When_Some_And_Predicate_True_Returns_Value();

	protected static void Test02(Func<Maybe<int>, Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Some(value);
		var predicate = Substitute.For<Func<int, bool>>();
		_ = predicate.Invoke(Arg.Any<int>()).Returns(true);

		// Act
		var result = act(maybe, predicate);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}

	public abstract void Test03_When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseMsg();

	protected static void Test03(Func<Maybe<string>, Func<string, bool>, Maybe<string>> act)
	{
		// Arrange
		var value = F.Rnd.Str;
		var maybe = Some(value);
		var predicate = Substitute.For<Func<string, bool>>();
		_ = predicate.Invoke(Arg.Any<string>()).Returns(false);

		// Act
		var result = act(maybe, predicate);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<FilterPredicateWasFalseMsg>(none);
	}

	public abstract void Test04_When_None_Returns_None_With_Original_Reason();

	protected static void Test04(Func<Maybe<int>, Func<int, bool>, Maybe<int>> act)
	{
		// Arrange
		var reason = new TestMsg();
		var maybe = None<int>(reason);
		var predicate = Substitute.For<Func<int, bool>>();

		// Act
		var result = act(maybe, predicate);

		// Assert
		var none = result.AssertNone();
		Assert.Same(reason, none);
		_ = predicate.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>());
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg : Msg;
}

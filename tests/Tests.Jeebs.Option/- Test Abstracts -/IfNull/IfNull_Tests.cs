// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using static F.MaybeF;

namespace Jeebs_Tests;

public abstract class IfNull_Tests
{
	public abstract void Test00_Exception_In_IfNull_Func_Returns_None_With_UnhandledExceptionMsg();

	protected static void Test00(Func<Maybe<object?>, Func<Maybe<object?>>, Maybe<object?>> act)
	{
		// Arrange
		var some = Some<object>(null, true);
		var none = None<object?, M.NullValueMsg>();
		var throws = Substitute.For<Func<Maybe<object?>>>();
		throws.Invoke().Throws<Exception>();

		// Act
		var r0 = act(some, throws);
		var r1 = act(none, throws);

		// Assert
		var n0 = r0.AssertNone();
		Assert.IsType<M.UnhandledExceptionMsg>(n0);
		var n1 = r1.AssertNone();
		Assert.IsType<M.UnhandledExceptionMsg>(n1);
	}

	public abstract void Test01_Some_With_Null_Value_Runs_IfNull_Func();

	protected static void Test01(Func<Maybe<object?>, Func<Maybe<object?>>, Maybe<object?>> act)
	{
		// Arrange
		var maybe = Some<object>(null, true);
		var ifNull = Substitute.For<Func<Maybe<object?>>>();

		// Act
		act(maybe, ifNull);

		// Assert
		ifNull.Received().Invoke();
	}

	public abstract void Test02_None_With_NullValueMsg_Runs_IfNull_Func();

	protected static void Test02(Func<Maybe<object>, Func<Maybe<object>>, Maybe<object>> act)
	{
		// Arrange
		var maybe = None<object, M.NullValueMsg>();
		var ifNull = Substitute.For<Func<Maybe<object>>>();

		// Act
		act(maybe, ifNull);

		// Assert
		ifNull.Received().Invoke();
	}

	public abstract void Test03_Some_With_Null_Value_Runs_IfNull_Func_Returns_None_With_Reason();

	protected static void Test03(Func<Maybe<object?>, Func<Msg>, Maybe<object?>> act)
	{
		// Arrange
		var maybe = Some<object>(null, true);
		var ifNull = Substitute.For<Func<Msg>>();
		var msg = new TestMsg();
		ifNull.Invoke().Returns(msg);

		// Act
		var result = act(maybe, ifNull);

		// Assert
		ifNull.Received().Invoke();
		var none = result.AssertNone();
		Assert.Same(msg, none);
	}

	public abstract void Test04_None_With_NullValueMsg_Runs_IfNull_Func_Returns_None_With_Reason();

	protected static void Test04(Func<Maybe<object>, Func<Msg>, Maybe<object>> act)
	{
		// Arrange
		var maybe = None<object, M.NullValueMsg>();
		var ifNull = Substitute.For<Func<Msg>>();
		var msg = new TestMsg();
		ifNull.Invoke().Returns(msg);

		// Act
		var result = act(maybe, ifNull);

		// Assert
		ifNull.Received().Invoke();
		var none = result.AssertNone();
		Assert.Same(msg, none);
	}

	public sealed record class TestMsg : Msg;
}

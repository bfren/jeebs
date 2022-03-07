// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using static F.MaybeF;

namespace Jeebs_Tests;

public abstract class IfNullAsync_Tests
{
	public abstract Task Test00_Exception_In_NullValue_Func_Returns_None_With_UnhandledExceptionMsg();

	protected static async Task Test00(Func<Maybe<object?>, Func<Task<Maybe<object?>>>, Task<Maybe<object?>>> act)
	{
		// Arrange
		var some = Some<object>(null, true);
		var none = None<object?, M.NullValueMsg>();
		var throws = Substitute.For<Func<Task<Maybe<object?>>>>();
		throws.Invoke().Throws<Exception>();

		// Act
		var r0 = await act(some, throws).ConfigureAwait(false);
		var r1 = await act(none, throws).ConfigureAwait(false);

		// Assert
		var n0 = r0.AssertNone();
		Assert.IsType<M.UnhandledExceptionMsg>(n0);
		var n1 = r1.AssertNone();
		Assert.IsType<M.UnhandledExceptionMsg>(n1);
	}

	public abstract Task Test01_Some_With_Null_Value_Runs_IfNull_Func();

	protected static async Task Test01(Func<Maybe<object?>, Func<Task<Maybe<object?>>>, Task<Maybe<object?>>> act)
	{
		// Arrange
		var some = Some<object>(null, true);
		var ifNull = Substitute.For<Func<Task<Maybe<object?>>>>();

		// Act
		await act(some, ifNull).ConfigureAwait(false);

		// Assert
		await ifNull.Received().Invoke().ConfigureAwait(false);
	}

	public abstract Task Test02_None_With_NullValueMsg_Runs_IfNull_Func();

	protected static async Task Test02(Func<Maybe<object>, Func<Task<Maybe<object>>>, Task<Maybe<object>>> act)
	{
		// Arrange
		var none = None<object, M.NullValueMsg>();
		var ifNull = Substitute.For<Func<Task<Maybe<object>>>>();

		// Act
		await act(none, ifNull).ConfigureAwait(false);

		// Assert
		await ifNull.Received().Invoke().ConfigureAwait(false);
	}

	public abstract Task Test03_Some_With_Null_Value_Runs_IfNull_Func_Returns_None_With_Reason();

	protected static async Task Test03(Func<Maybe<object?>, Func<Msg>, Task<Maybe<object?>>> act)
	{
		// Arrange
		var maybe = Some<object>(null, true);
		var ifNull = Substitute.For<Func<Msg>>();
		var msg = new TestMsg();
		ifNull.Invoke().Returns(msg);

		// Act
		var result = await act(maybe, ifNull).ConfigureAwait(false);

		// Assert
		ifNull.Received().Invoke();
		var none = result.AssertNone();
		Assert.Same(msg, none);
	}

	public abstract Task Test04_None_With_NullValueMsg_Runs_IfNull_Func_Returns_None_With_Reason();

	protected static async Task Test04(Func<Maybe<object>, Func<Msg>, Task<Maybe<object>>> act)
	{
		// Arrange
		var maybe = None<object, M.NullValueMsg>();
		var ifNull = Substitute.For<Func<Msg>>();
		var msg = new TestMsg();
		ifNull.Invoke().Returns(msg);

		// Act
		var result = await act(maybe, ifNull).ConfigureAwait(false);

		// Assert
		ifNull.Received().Invoke();
		var none = result.AssertNone();
		Assert.Same(msg, none);
	}

	public sealed record class TestMsg : Msg;
}

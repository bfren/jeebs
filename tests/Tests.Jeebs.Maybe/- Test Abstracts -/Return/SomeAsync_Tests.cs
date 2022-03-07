// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.MaybeF;
using static F.MaybeF.M;

namespace Jeebs_Tests;

public abstract class SomeAsync_Tests
{
	public abstract Task Test00_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg();

	protected static async Task Test00(Func<Func<Task<int>>, Handler, Task<Maybe<int>>> act)
	{
		// Arrange
		var throwFunc = Task<int> () => throw new MaybeTestException();

		// Act
		var result = await act(throwFunc, DefaultHandler).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<UnhandledExceptionMsg>(none);
	}

	public abstract Task Test01_Nullable_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg();

	protected static async Task Test01(Func<Func<Task<int?>>, bool, Handler, Task<Maybe<int?>>> act)
	{
		// Arrange
		var throwFunc = Task<int?> () => throw new MaybeTestException();

		// Act
		var r0 = await act(throwFunc, true, DefaultHandler).ConfigureAwait(false);
		var r1 = await act(throwFunc, false, DefaultHandler).ConfigureAwait(false);

		// Assert
		var n0 = r0.AssertNone();
		_ = Assert.IsType<UnhandledExceptionMsg>(n0);
		var n1 = r1.AssertNone();
		_ = Assert.IsType<UnhandledExceptionMsg>(n1);
	}

	public abstract Task Test02_Exception_Thrown_With_Handler_Returns_None_Calls_Handler();

	protected static async Task Test02(Func<Func<Task<int>>, Handler, Task<Maybe<int>>> act)
	{
		// Arrange
		var handler = Substitute.For<Handler>();
		var exception = new Exception();
		var throwFunc = Task<int> () => throw exception;

		// Act
		var result = await act(throwFunc, handler).ConfigureAwait(false);

		// Assert
		_ = result.AssertNone();
		_ = handler.Received().Invoke(exception);
	}

	public abstract Task Test03_Nullable_Exception_Thrown_With_Handler_Returns_None_Calls_Handler();

	protected static async Task Test03(Func<Func<Task<int?>>, bool, Handler, Task<Maybe<int?>>> act)
	{
		// Arrange
		var handler = Substitute.For<Handler>();
		var exception = new Exception();
		var throwFunc = Task<int?> () => throw exception;

		// Act
		var r0 = await act(throwFunc, true, handler).ConfigureAwait(false);
		var r1 = await act(throwFunc, false, handler).ConfigureAwait(false);

		// Assert
		_ = r0.AssertNone();
		_ = r1.AssertNone();
		_ = handler.Received(2).Invoke(exception);
	}

	public abstract Task Test04_Null_Input_Returns_None();

	protected static async Task Test04(Func<Func<Task<int?>>, Handler, Task<Maybe<int?>>> act)
	{
		// Arrange
		var value = Task<int?> () => Task.FromResult<int?>(null);

		// Act
		var result = await act(value, DefaultHandler).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<NullValueMsg>(none);
	}

	public abstract Task Test05_Nullable_Allow_Null_False_Null_Input_Returns_None_With_AllowNullWasFalseMsg();

	protected static async Task Test05(Func<Func<Task<int?>>, bool, Handler, Task<Maybe<int?>>> act)
	{
		// Arrange
		var value = Task<int?> () => Task.FromResult<int?>(null);

		// Act
		var result = await act(value, false, DefaultHandler).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<AllowNullWasFalseMsg>(none);
	}

	public abstract Task Test06_Nullable_Allow_Null_True_Null_Input_Returns_Some_With_Null_Value();

	protected static async Task Test06(Func<Func<Task<int?>>, bool, Handler, Task<Maybe<int?>>> act)
	{
		// Arrange
		var value = Task<int?> () => Task.FromResult<int?>(null);

		// Act
		var result = await act(value, true, DefaultHandler).ConfigureAwait(false);

		// Assert
		var some = result.AssertSome();
		Assert.Null(some);
	}

	public abstract Task Test07_Not_Null_Returns_Some();

	protected static async Task Test07(Func<Func<Task<object>>, Handler, Task<Maybe<object>>> act)
	{
		// Arrange
		var v0 = F.Rnd.Str;
		var f0 = Task<object> () => Task.FromResult<object>(v0);

		var v1 = F.Rnd.Int;
		var f1 = Task<object> () => Task.FromResult<object>(v1);

		var v2 = F.Rnd.Guid;
		var f2 = Task<object> () => Task.FromResult<object>(v2);

		// Act
		var r0 = await act(f0, DefaultHandler).ConfigureAwait(false);
		var r1 = await act(f1, DefaultHandler).ConfigureAwait(false);
		var r2 = await act(f2, DefaultHandler).ConfigureAwait(false);

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(v0, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(v1, s1);
		var s2 = r2.AssertSome();
		Assert.Equal(v2, s2);
	}

	public abstract Task Test08_Nullable_Not_Null_Returns_Some();

	protected static async Task Test08(Func<Func<Task<object?>>, bool, Handler, Task<Maybe<object?>>> act)
	{
		// Arrange
		var v0 = F.Rnd.Str;
		var f0 = Task<object?> () => Task.FromResult<object?>(v0);

		var v1 = F.Rnd.Int;
		var f1 = Task<object?> () => Task.FromResult<object?>(v1);

		var v2 = F.Rnd.Guid;
		var f2 = Task<object?> () => Task.FromResult<object?>(v2);

		// Act
		var r0 = await act(f0, false, DefaultHandler).ConfigureAwait(false);
		var r1 = await act(f1, false, DefaultHandler).ConfigureAwait(false);
		var r2 = await act(f2, false, DefaultHandler).ConfigureAwait(false);

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(v0, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(v1, s1);
		var s2 = r2.AssertSome();
		Assert.Equal(v2, s2);
	}
}

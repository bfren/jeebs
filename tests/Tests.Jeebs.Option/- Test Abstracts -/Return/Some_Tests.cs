// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.MaybeF;
using static F.MaybeF.M;

namespace Jeebs_Tests;

public abstract class Some_Tests
{
	public abstract void Test00_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg();

	protected static void Test00(Func<Func<int>, Handler, Maybe<int>> act)
	{
		// Arrange
		var throwFunc = int () => throw new MaybeTestException();

		// Act
		var result = act(throwFunc, DefaultHandler);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<UnhandledExceptionMsg>(none);
	}

	public abstract void Test01_Nullable_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg();

	protected static void Test01(Func<Func<int?>, bool, Handler, Maybe<int?>> act)
	{
		// Arrange
		var throwFunc = int? () => throw new MaybeTestException();

		// Act
		var r0 = act(throwFunc, true, DefaultHandler);
		var r1 = act(throwFunc, false, DefaultHandler);

		// Assert
		var n0 = r0.AssertNone();
		Assert.IsType<UnhandledExceptionMsg>(n0);
		var n1 = r1.AssertNone();
		Assert.IsType<UnhandledExceptionMsg>(n1);
	}

	public abstract void Test02_Exception_Thrown_With_Handler_Returns_None_Calls_Handler();

	protected static void Test02(Func<Func<int>, Handler, Maybe<int>> act)
	{
		// Arrange
		var handler = Substitute.For<Handler>();
		var exception = new Exception();
		var throwFunc = int () => throw exception;

		// Act
		var result = act(throwFunc, handler);

		// Assert
		result.AssertNone();
		handler.Received().Invoke(exception);
	}

	public abstract void Test03_Nullable_Exception_Thrown_With_Handler_Returns_None_Calls_Handler();

	protected static void Test03(Func<Func<int?>, bool, Handler, Maybe<int?>> act)
	{
		// Arrange
		var handler = Substitute.For<Handler>();
		var exception = new Exception();
		var throwFunc = int? () => throw exception;

		// Act
		var r0 = act(throwFunc, true, handler);
		var r1 = act(throwFunc, false, handler);

		// Assert
		r0.AssertNone();
		r1.AssertNone();
		handler.Received(2).Invoke(exception);
	}

	public abstract void Test04_Null_Input_Value_Returns_None();

	protected static void Test04(Func<int?, Maybe<int?>> act)
	{
		// Arrange
		int? value = null;

		// Act
		var result = act(value);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<NullValueMsg>(none);
	}

	public abstract void Test05_Null_Input_Func_Returns_None();

	protected static void Test05(Func<Func<int?>, Handler, Maybe<int?>> act)
	{
		// Arrange
		var value = int? () => null;

		// Act
		var result = act(value, DefaultHandler);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<NullValueMsg>(none);
	}

	public abstract void Test06_Nullable_Allow_Null_False_Null_Input_Value_Returns_None_With_AllowNullWasFalseMsg();

	protected static void Test06(Func<int?, bool, Maybe<int?>> act)
	{
		// Arrange
		int? value = null;

		// Act
		var result = act(value, false);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<AllowNullWasFalseMsg>(none);
	}

	public abstract void Test07_Nullable_Allow_Null_False_Null_Input_Func_Returns_None_With_AllowNullWasFalseMsg();

	protected static void Test07(Func<Func<int?>, bool, Handler, Maybe<int?>> act)
	{
		// Arrange
		var value = int? () => null;

		// Act
		var result = act(value, false, DefaultHandler);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<AllowNullWasFalseMsg>(none);
	}

	public abstract void Test08_Nullable_Allow_Null_True_Null_Input_Value_Returns_Some_With_Null_Value();

	protected static void Test08(Func<int?, bool, Maybe<int?>> act)
	{
		// Arrange
		int? value = null;

		// Act
		var result = act(value, true);

		// Assert
		var some = result.AssertSome();
		Assert.Null(some);
	}

	public abstract void Test09_Nullable_Allow_Null_True_Null_Input_Func_Returns_Some_With_Null_Value();

	protected static void Test09(Func<Func<int?>, bool, Handler, Maybe<int?>> act)
	{
		// Arrange
		var value = int? () => null;

		// Act
		var result = act(value, true, DefaultHandler);

		// Assert
		var some = result.AssertSome();
		Assert.Null(some);
	}

	public abstract void Test10_Not_Null_Value_Returns_Some();

	protected static void Test10(Func<object, Maybe<object>> act)
	{
		// Arrange
		object v0 = F.Rnd.Str;
		object v1 = F.Rnd.Int;
		object v2 = F.Rnd.Guid;

		// Act
		var r0 = act(v0);
		var r1 = act(v1);
		var r2 = act(v2);

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(v0, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(v1, s1);
		var s2 = r2.AssertSome();
		Assert.Equal(v2, s2);
	}

	public abstract void Test11_Not_Null_Func_Returns_Some();

	protected static void Test11(Func<Func<object>, Handler, Maybe<object>> act)
	{
		// Arrange
		var v0 = F.Rnd.Str;
		var f0 = object () => v0;

		var v1 = F.Rnd.Int;
		var f1 = object () => v1;

		var v2 = F.Rnd.Guid;
		var f2 = object () => v2;

		// Act
		var r0 = act(f0, DefaultHandler);
		var r1 = act(f1, DefaultHandler);
		var r2 = act(f2, DefaultHandler);

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(v0, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(v1, s1);
		var s2 = r2.AssertSome();
		Assert.Equal(v2, s2);
	}

	public abstract void Test12_Nullable_Not_Null_Value_Returns_Some();

	protected static void Test12(Func<object?, bool, Maybe<object?>> act)
	{
		// Arrange
		object? v0 = F.Rnd.Str;
		object? v1 = F.Rnd.Int;
		object? v2 = F.Rnd.Guid;

		// Act
		var r0 = act(v0, false);
		var r1 = act(v1, false);
		var r2 = act(v2, false);

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(v0, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(v1, s1);
		var s2 = r2.AssertSome();
		Assert.Equal(v2, s2);
	}

	public abstract void Test13_Nullable_Not_Null_Func_Returns_Some();

	protected static void Test13(Func<Func<object?>, bool, Handler, Maybe<object?>> act)
	{
		// Arrange
		var v0 = F.Rnd.Str;
		var f0 = object? () => v0;

		var v1 = F.Rnd.Int;
		var f1 = object? () => v1;

		var v2 = F.Rnd.Guid;
		var f2 = object? () => v2;

		// Act
		var r0 = act(f0, false, DefaultHandler);
		var r1 = act(f1, false, DefaultHandler);
		var r2 = act(f2, false, DefaultHandler);

		// Assert
		var s0 = r0.AssertSome();
		Assert.Equal(v0, s0);
		var s1 = r1.AssertSome();
		Assert.Equal(v1, s1);
		var s2 = r2.AssertSome();
		Assert.Equal(v2, s2);
	}
}

// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace Jeebs_Tests;

public abstract class SomeIf_Tests
{
	public abstract void Test00_Exception_Thrown_By_Predicate_With_Value_Calls_Handler_Returns_None();

	protected static void Test00(Func<Func<bool>, int, Handler, Option<int>> act)
	{
		// Arrange
		var handler = Substitute.For<Handler>();
		var exception = new Exception();
		bool throwFunc() => throw exception;

		// Act
		var result = act(throwFunc, F.Rnd.Int, handler);

		// Assert
		result.AssertNone();
		handler.Received().Invoke(exception);
	}

	public abstract void Test01_Exception_Thrown_By_Predicate_With_Value_Func_Calls_Handler_Returns_None();

	protected static void Test01(Func<Func<bool>, Func<int>, Handler, Option<int>> act)
	{
		// Arrange
		var handler = Substitute.For<Handler>();
		var exception = new Exception();
		bool throwFunc() => throw exception;

		// Act
		var result = act(throwFunc, () => F.Rnd.Int, handler);

		// Assert
		result.AssertNone();
		handler.Received().Invoke(exception);
	}

	public abstract void Test02_Exception_Thrown_By_Value_Func_Calls_Handler_Returns_None();

	protected static void Test02(Func<Func<bool>, Func<int>, Handler, Option<int>> act)
	{
		// Arrange
		var handler = Substitute.For<Handler>();
		var exception = new Exception();
		int throwFunc() => throw exception;

		// Act
		var result = act(() => true, throwFunc, handler);

		// Assert
		result.AssertNone();
		handler.Received().Invoke(exception);
	}

	public abstract void Test03_Predicate_True_With_Value_Returns_Some();

	protected static void Test03(Func<Func<bool>, int, Handler, Option<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;

		// Act
		var result = act(() => true, value, DefaultHandler);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}

	public abstract void Test04_Predicate_True_With_Value_Func_Returns_Some();

	protected static void Test04(Func<Func<bool>, Func<int>, Handler, Option<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;

		// Act
		var result = act(() => true, () => value, DefaultHandler);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}

	public abstract void Test05_Predicate_False_With_Value_Returns_None_With_PredicateWasFalseMsg();

	protected static void Test05(Func<Func<bool>, int, Handler, Option<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;

		// Act
		var result = act(() => false, value, DefaultHandler);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<PredicateWasFalseMsg>(none);
	}

	public abstract void Test06_Predicate_False_With_Value_Func_Returns_None_With_PredicateWasFalseMsg();

	protected static void Test06(Func<Func<bool>, Func<int>, Handler, Option<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;

		// Act
		var result = act(() => false, () => value, DefaultHandler);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<PredicateWasFalseMsg>(none);
	}

	public abstract void Test07_Predicate_False_Bypasses_Value_Func();

	protected static void Test07(Func<Func<bool>, Func<int>, Handler, Option<int>> act)
	{
		// Arrange
		var getValue = Substitute.For<Func<int>>();

		// Act
		var result = act(() => false, getValue, DefaultHandler);

		// Assert
		result.AssertNone();
		getValue.DidNotReceive().Invoke();
	}
}

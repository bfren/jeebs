// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace Jeebs_Tests
{
	public abstract class ReturnIfAsync_Tests
	{
		public abstract Task Test00_Exception_Thrown_By_Predicate_With_Value_Calls_Handler_Returns_None();

		protected static async Task Test00(Func<Func<bool>, Task<int>, Handler, Task<Option<int>>> act)
		{
			// Arrange
			var handler = Substitute.For<Handler>();
			var exception = new Exception();
			bool throwFunc() => throw exception;

			// Act
			var result = await act(throwFunc, Task.FromResult(F.Rnd.Int), handler);

			// Assert
			result.AssertNone();
			handler.Received().Invoke(exception);
		}

		public abstract Task Test01_Exception_Thrown_By_Predicate_With_Value_Func_Calls_Handler_Returns_None();

		protected static async Task Test01(Func<Func<bool>, Func<Task<int>>, Handler, Task<Option<int>>> act)
		{
			// Arrange
			var handler = Substitute.For<Handler>();
			var exception = new Exception();
			bool throwFunc() => throw exception;

			// Act
			var result = await act(throwFunc, () => Task.FromResult(F.Rnd.Int), handler);

			// Assert
			result.AssertNone();
			handler.Received().Invoke(exception);
		}

		public abstract Task Test02_Exception_Thrown_By_Value_Func_Calls_Handler_Returns_None();

		protected static async Task Test02(Func<Func<bool>, Func<Task<int>>, Handler, Task<Option<int>>> act)
		{
			// Arrange
			var handler = Substitute.For<Handler>();
			var exception = new Exception();
			Task<int> throwFunc() => throw exception;

			// Act
			var result = await act(() => true, throwFunc, handler);

			// Assert
			result.AssertNone();
			handler.Received().Invoke(exception);
		}

		public abstract Task Test03_Predicate_True_With_Value_Returns_Some();

		protected static async Task Test03(Func<Func<bool>, Task<int>, Handler, Task<Option<int>>> act)
		{
			// Arrange
			var value = F.Rnd.Int;

			// Act
			var result = await act(() => true, Task.FromResult(value), DefaultHandler);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(value, some);
		}

		public abstract Task Test04_Predicate_True_With_Value_Func_Returns_Some();

		protected static async Task Test04(Func<Func<bool>, Func<Task<int>>, Handler, Task<Option<int>>> act)
		{
			// Arrange
			var value = F.Rnd.Int;

			// Act
			var result = await act(() => true, () => Task.FromResult(value), DefaultHandler);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(value, some);
		}

		public abstract Task Test05_Predicate_False_With_Value_Returns_None_With_PredicateWasFalseMsg();

		protected static async Task Test05(Func<Func<bool>, Task<int>, Handler, Task<Option<int>>> act)
		{
			// Arrange
			var value = F.Rnd.Int;

			// Act
			var result = await act(() => false, Task.FromResult(value), DefaultHandler);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<PredicateWasFalseMsg>(none);
		}

		public abstract Task Test06_Predicate_False_With_Value_Func_Returns_None_With_PredicateWasFalseMsg();

		protected static async Task Test06(Func<Func<bool>, Func<Task<int>>, Handler, Task<Option<int>>> act)
		{
			// Arrange
			var value = F.Rnd.Int;

			// Act
			var result = await act(() => false, () => Task.FromResult(value), DefaultHandler);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<PredicateWasFalseMsg>(none);
		}

		public abstract Task Test07_Predicate_False_Bypasses_Value_Func();

		protected static async Task Test07(Func<Func<bool>, Func<Task<int>>, Handler, Task<Option<int>>> act)
		{
			// Arrange
			var getValue = Substitute.For<Func<Task<int>>>();

			// Act
			var result = await act(() => false, getValue, DefaultHandler);

			// Assert
			result.AssertNone();
			await getValue.DidNotReceive().Invoke();
		}
	}
}

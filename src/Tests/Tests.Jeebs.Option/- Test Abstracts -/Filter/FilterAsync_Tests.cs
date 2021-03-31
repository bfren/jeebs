// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace Jeebs_Tests
{
	public abstract class FilterAsync_Tests
	{
		public abstract Task Test00_If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg();

		protected static async Task Test00(Func<Option<int>, Task<Option<int>>> act)
		{
			// Arrange
			var option = new FakeOption();

			// Act
			var result = await act(option);

			// Assert
			var none = result.AssertNone();
			var msg = Assert.IsType<UnhandledExceptionMsg>(none);
			Assert.IsType<UnknownOptionException>(msg.Exception);
		}

		public abstract Task Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg();

		protected static async Task Test01(Func<Option<string>, Func<string, Task<bool>>, Task<Option<string>>> act)
		{
			// Arrange
			var option = Return(F.Rnd.Str);
			var exception = new Exception();
			Task<bool> throwFunc(string _) => throw exception;

			// Act
			var result = await act(option, throwFunc);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(none);
		}

		public abstract Task Test02_When_Some_And_Predicate_True_Returns_Value();

		protected static async Task Test02(Func<Option<int>, Func<int, Task<bool>>, Task<Option<int>>> act)
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var predicate = Substitute.For<Func<int, Task<bool>>>();
			predicate.Invoke(Arg.Any<int>()).Returns(true);

			// Act
			var result = await act(option, predicate);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(value, some);
		}

		public abstract Task Test03_When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseMsg();

		protected static async Task Test03(Func<Option<string>, Func<string, Task<bool>>, Task<Option<string>>> act)
		{
			// Arrange
			var value = F.Rnd.Str;
			var option = Return(value);
			var predicate = Substitute.For<Func<string, Task<bool>>>();
			predicate.Invoke(Arg.Any<string>()).Returns(false);

			// Act
			var result = await act(option, predicate);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<FilterPredicateWasFalseMsg>(none);
		}

		public abstract Task Test04_When_None_Returns_None_With_Original_Reason();

		protected static async Task Test04(Func<Option<int>, Func<int, Task<bool>>, Task<Option<int>>> act)
		{
			// Arrange
			var reason = new TestMsg();
			var option = None<int>(reason);
			var predicate = Substitute.For<Func<int, Task<bool>>>();

			// Act
			var result = await act(option, predicate);

			// Assert
			var none = result.AssertNone();
			Assert.Same(reason, none);
			await predicate.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>());
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}

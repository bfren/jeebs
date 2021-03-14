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

namespace F.OptionF_Tests
{
	public class FilterAsync_Tests
	{
		[Fact]
		public async Task If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = new FakeOption();

			// Act
			var r0 = await FilterAsync(option, Substitute.For<Func<int, Task<bool>>>(), null);
			var r1 = await FilterAsync(option.AsTask, Substitute.For<Func<int, Task<bool>>>(), null);

			// Assert
			var n0 = Assert.IsType<None<int>>(r0);
			var m0 = Assert.IsType<UnhandledExceptionMsg>(n0.Reason);
			Assert.IsType<UnknownOptionException>(m0.Exception);
			var n1 = Assert.IsType<None<int>>(r1);
			var m1 = Assert.IsType<UnhandledExceptionMsg>(n1.Reason);
			Assert.IsType<UnknownOptionException>(m1.Exception);
		}

		[Fact]
		public async Task Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = Return(Rnd.Str);
			var exception = new Exception();
			Task<bool> throwFunc(string _) => throw exception;

			// Act
			var r0 = await FilterAsync(option, throwFunc, null);
			var r1 = await FilterAsync(option.AsTask, throwFunc, null);

			// Assert
			var n0 = Assert.IsType<None<string>>(r0);
			Assert.IsType<UnhandledExceptionMsg>(n0.Reason);
			var n1 = Assert.IsType<None<string>>(r1);
			Assert.IsType<UnhandledExceptionMsg>(n1.Reason);
		}

		[Fact]
		public async Task Exception_Thrown_With_Handler_Returns_None_Calls_Handler()
		{
			// Arrange
			var option = Return(Rnd.Str);
			var handler = Substitute.For<Handler>();
			var exception = new Exception();
			Task<bool> throwFunc(string _) => throw exception;

			// Act
			var r0 = await FilterAsync(option, throwFunc, handler);
			var r1 = await FilterAsync(option.AsTask, throwFunc, handler);

			// Assert
			Assert.IsType<None<string>>(r0);
			Assert.IsType<None<string>>(r1);
			handler.Received(2).Invoke(exception);
		}

		[Fact]
		public async Task When_Some_And_Predicate_True_Returns_Value()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value);

			// Act
			var r0 = await FilterAsync(option, x => Task.FromResult(x == value), null);
			var r1 = await FilterAsync(option.AsTask, x => Task.FromResult(x == value), null);

			// Assert
			var s0 = Assert.IsType<Some<int>>(r0);
			Assert.Equal(value, s0.Value);
			var s1 = Assert.IsType<Some<int>>(r1);
			Assert.Equal(value, s1.Value);
		}

		[Fact]
		public async Task When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseMsg()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value);

			// Act
			var r0 = await FilterAsync(option, x => Task.FromResult(x != value), null);
			var r1 = await FilterAsync(option.AsTask, x => Task.FromResult(x != value), null);

			// Assert
			var n0 = Assert.IsType<None<int>>(r0);
			Assert.IsType<FilterPredicateWasFalseMsg>(n0.Reason);
			var n1 = Assert.IsType<None<int>>(r1);
			Assert.IsType<FilterPredicateWasFalseMsg>(n1.Reason);
		}

		[Fact]
		public async Task When_None_Returns_None_With_Original_Reason()
		{
			// Arrange
			var reason = new TestMsg();
			var option = None<int>(reason);
			var predicate = Substitute.For<Func<int, Task<bool>>>();

			// Act
			var r0 = await FilterAsync(option, predicate, null);
			var r1 = await FilterAsync(option.AsTask, predicate, null);

			// Assert
			var n0 = Assert.IsType<None<int>>(r0);
			Assert.Same(reason, n0.Reason);
			var n1 = Assert.IsType<None<int>>(r1);
			Assert.Same(reason, n1.Reason);
			await predicate.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>());
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}

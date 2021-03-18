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
			var r0 = await FilterAsync(option, Substitute.For<Func<int, Task<bool>>>());
			var r1 = await FilterAsync(option.AsTask, Substitute.For<Func<int, Task<bool>>>());

			// Assert
			var n0 = r0.AssertNone();
			var m0 = Assert.IsType<UnhandledExceptionMsg>(n0);
			Assert.IsType<UnknownOptionException>(m0.Exception);
			var n1 = r1.AssertNone();
			var m1 = Assert.IsType<UnhandledExceptionMsg>(n1);
			Assert.IsType<UnknownOptionException>(m1.Exception);
		}

		[Fact]
		public async Task Exception_Thrown_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = Return(Rnd.Str);
			var exception = new Exception();
			Task<bool> throwFunc(string _) => throw exception;

			// Act
			var r0 = await FilterAsync(option, throwFunc);
			var r1 = await FilterAsync(option.AsTask, throwFunc);

			// Assert
			var n0 = r0.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(n0);
			var n1 = r1.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(n1);
		}

		[Fact]
		public async Task When_Some_And_Predicate_True_Returns_Value()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value);

			// Act
			var r0 = await FilterAsync(option, x => Task.FromResult(x == value));
			var r1 = await FilterAsync(option.AsTask, x => Task.FromResult(x == value));

			// Assert
			var s0 = r0.AssertSome();
			Assert.Equal(value, s0);
			var s1 = r1.AssertSome();
			Assert.Equal(value, s1);
		}

		[Fact]
		public async Task When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseMsg()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value);

			// Act
			var r0 = await FilterAsync(option, x => Task.FromResult(x != value));
			var r1 = await FilterAsync(option.AsTask, x => Task.FromResult(x != value));

			// Assert
			var n0 = r0.AssertNone();
			Assert.IsType<FilterPredicateWasFalseMsg>(n0);
			var n1 = r1.AssertNone();
			Assert.IsType<FilterPredicateWasFalseMsg>(n1);
		}

		[Fact]
		public async Task When_None_Returns_None_With_Original_Reason()
		{
			// Arrange
			var reason = new TestMsg();
			var option = None<int>(reason);
			var predicate = Substitute.For<Func<int, Task<bool>>>();

			// Act
			var r0 = await FilterAsync(option, predicate);
			var r1 = await FilterAsync(option.AsTask, predicate);

			// Assert
			var n0 = r0.AssertNone();
			Assert.Same(reason, n0);
			var n1 = r1.AssertNone();
			Assert.Same(reason, n1);
			await predicate.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>());
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}

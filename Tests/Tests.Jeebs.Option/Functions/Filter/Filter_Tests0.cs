// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace F.OptionF_Tests
{
	public partial class Filter_Tests
	{
		[Fact]
		public void If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = new FakeOption();

			// Act
			var result = Filter(option, Substitute.For<Func<int, bool>>(), null);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			var msg = Assert.IsType<UnhandledExceptionMsg>(none.Reason);
			Assert.IsType<UnknownOptionException>(msg.Exception);
		}

		[Fact]
		public void Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = Return(Rnd.Str);
			var exception = new Exception();
			bool throwFunc(string _) => throw exception;

			// Act
			var result = Filter(option, throwFunc, null);

			// Assert
			var none = Assert.IsType<None<string>>(result);
			Assert.IsType<UnhandledExceptionMsg>(none.Reason);
		}

		[Fact]
		public void Exception_Thrown_With_Handler_Returns_None_Calls_Handler()
		{
			// Arrange
			var option = Return(Rnd.Str);
			var handler = Substitute.For<Handler>();
			var exception = new Exception();
			bool throwFunc(string _) => throw exception;

			// Act
			var result = Filter(option, throwFunc, handler);

			// Assert
			Assert.IsType<None<string>>(result);
			handler.Received().Invoke(exception);
		}

		[Fact]
		public void When_Some_And_Predicate_True_Returns_Value()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value);
			var predicate = Substitute.For<Func<int, bool>>();
			predicate.Invoke(Arg.Any<int>()).Returns(true);

			// Act
			var result = Filter(option, predicate, null);

			// Assert
			var some = Assert.IsType<Some<int>>(result);
			Assert.Equal(value, some.Value);
		}

		[Fact]
		public void When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseMsg()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value);
			var predicate = Substitute.For<Func<int, bool>>();
			predicate.Invoke(Arg.Any<int>()).Returns(false);

			// Act
			var result = Filter(option, predicate, null);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.IsType<FilterPredicateWasFalseMsg>(none.Reason);
		}

		[Fact]
		public void When_None_Returns_None_With_Original_Reason()
		{
			// Arrange
			var reason = new TestMsg();
			var option = None<int>(reason);
			var predicate = Substitute.For<Func<int, bool>>();

			// Act
			var result = Filter(option, predicate, null);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.Same(reason, none.Reason);
			predicate.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>());
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}

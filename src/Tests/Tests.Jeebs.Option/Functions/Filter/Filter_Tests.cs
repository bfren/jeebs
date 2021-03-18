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
			var result = Filter(option, Substitute.For<Func<int, bool>>());

			// Assert
			var none = result.AssertNone();
			var msg = Assert.IsType<UnhandledExceptionMsg>(none);
			Assert.IsType<UnknownOptionException>(msg.Exception);
		}

		[Fact]
		public void Exception_Thrown_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = Return(Rnd.Str);
			var exception = new Exception();
			bool throwFunc(string _) => throw exception;

			// Act
			var result = Filter(option, throwFunc);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(none);
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
			var result = Filter(option, predicate);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(value, some);
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
			var result = Filter(option, predicate);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<FilterPredicateWasFalseMsg>(none);
		}

		[Fact]
		public void When_None_Returns_None_With_Original_Reason()
		{
			// Arrange
			var reason = new TestMsg();
			var option = None<int>(reason);
			var predicate = Substitute.For<Func<int, bool>>();

			// Act
			var result = Filter(option, predicate);

			// Assert
			var none = result.AssertNone();
			Assert.Same(reason, none);
			predicate.DidNotReceiveWithAnyArgs().Invoke(Arg.Any<int>());
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}

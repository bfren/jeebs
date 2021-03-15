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
	public class Map_Tests
	{
		[Fact]
		public void If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = new FakeOption();
			var map = Substitute.For<Func<int, string>>();

			// Act
			var result = Map(option, map, DefaultHandler);

			// Assert
			var none = result.AssertNone();
			var msg = Assert.IsType<UnhandledExceptionMsg>(none);
			Assert.IsType<UnknownOptionException>(msg.Exception);
		}

		[Fact]
		public void Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = Return(Rnd.Str);
			var exception = new Exception();
			int throwFunc() => throw exception;

			// Act
			var r0 = Map(option, _ => throwFunc(), DefaultHandler);
			var r1 = Map(throwFunc, DefaultHandler);

			// Assert
			var n0 = r0.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(n0);
			var n1 = r1.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(n1);
		}

		[Fact]
		public void Exception_Thrown_With_Handler_Returns_None_Calls_Handler()
		{
			// Arrange
			var option = Return(Rnd.Str);
			var handler = Substitute.For<Handler>();
			var exception = new Exception();
			int throwFunc() => throw exception;

			// Act
			var r0 = Map(option, _ => throwFunc(), handler);
			var r1 = Map(throwFunc, handler);

			// Assert
			r0.AssertNone();
			r1.AssertNone();
			handler.Received(2).Invoke(exception);
		}

		[Fact]
		public void If_None_Gets_None()
		{
			// Arrange
			var option = None<int>(true);
			var map = Substitute.For<Func<int, string>>();

			// Act
			var result = Map(option, map, DefaultHandler);

			// Assert
			result.AssertNone();
		}

		[Fact]
		public void If_None_With_Reason_Gets_None_With_Same_Reason()
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var map = Substitute.For<Func<int, string>>();

			// Act
			var result = Map(option, map, DefaultHandler);

			// Assert
			var none = result.AssertNone();
			Assert.Same(msg, none);
		}

		[Fact]
		public void If_Some_Runs_Map_Function()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value);
			var map = Substitute.For<Func<int, string>>();

			// Act
			Map(option, map, DefaultHandler);
			Map(() => map(value), DefaultHandler);

			// Assert
			map.Received(2).Invoke(value);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
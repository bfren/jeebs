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
			var result = Map(option, map, null);

			// Assert
			var none = Assert.IsType<None<string>>(result);
			var msg = Assert.IsType<UnhandledExceptionMsg>(none.Reason);
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
			var r0 = Map(option, _ => throwFunc(), null);
			var r1 = Map(throwFunc);

			// Assert
			var n0 = Assert.IsType<None<int>>(r0);
			Assert.IsType<UnhandledExceptionMsg>(n0.Reason);
			var n1 = Assert.IsType<None<int>>(r1);
			Assert.IsType<UnhandledExceptionMsg>(n1.Reason);
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
			Assert.IsType<None<int>>(r0);
			Assert.IsType<None<int>>(r1);
			handler.Received(2).Invoke(exception);
		}

		[Fact]
		public void If_None_Gets_None()
		{
			// Arrange
			var option = None<int>(true);
			var map = Substitute.For<Func<int, string>>();

			// Act
			var result = Map(option, map, null);

			// Assert
			Assert.IsType<None<string>>(result);
		}

		[Fact]
		public void If_None_With_Reason_Gets_None_With_Same_Reason()
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var map = Substitute.For<Func<int, string>>();

			// Act
			var result = Map(option, map, null);

			// Assert
			var none = Assert.IsType<None<string>>(result);
			Assert.Same(msg, none.Reason);
		}

		[Fact]
		public void If_Some_Runs_Map_Function()
		{
			// Arrange
			var value = Rnd.Int;
			var option = Return(value);
			var map = Substitute.For<Func<int, string>>();

			// Act
			Map(option, map, Substitute.For<Handler>());
			Map(() => map(value));
			Map(() => map(value), Substitute.For<Handler>());

			// Assert
			map.Received(3).Invoke(value);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}
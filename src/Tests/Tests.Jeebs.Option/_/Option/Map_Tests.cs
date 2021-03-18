// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace Jeebs.Option_Tests
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
			var result = option.Map(map, DefaultHandler);

			// Assert
			var none = result.AssertNone();
			var msg = Assert.IsType<UnhandledExceptionMsg>(none);
			Assert.IsType<UnknownOptionException>(msg.Exception);
		}

		[Fact]
		public void Exception_Thrown_With_Handler_Calls_Handler()
		{
			// Arrange
			var option = Return(F.Rnd.Str);
			var handler = Substitute.For<Handler>();
			var exception = new Exception();

			// Act
			var result = option.Map<int>(_ => throw exception, handler);

			// Assert
			result.AssertNone();
			handler.Received().Invoke(exception);
		}

		[Fact]
		public void If_None_Gets_None()
		{
			// Arrange
			var option = None<int>(true);
			var map = Substitute.For<Func<int, string>>();

			// Act
			var result = option.Map(map, DefaultHandler);

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
			var result = option.Map(map, DefaultHandler);

			// Assert
			var none = result.AssertNone();
			Assert.Same(msg, none);
		}

		[Fact]
		public void If_Some_Runs_Map_Function()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var map = Substitute.For<Func<int, string>>();

			// Act
			option.Map(map, DefaultHandler);

			// Assert
			map.Received().Invoke(value);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}

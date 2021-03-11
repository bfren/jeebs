// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;

namespace Jeebs.Option_Tests
{
	public class DoMap_Tests
	{
		[Fact]
		public void If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = new FakeOption();
			var some = Substitute.For<Func<int, string>>();

			// Act
			var result = option.DoMap(some, null);

			// Assert
			var none = Assert.IsType<None<string>>(result);
			var msg = Assert.IsType<Jm.Option.UnhandledExceptionMsg>(none.Reason);
			Assert.IsType<Jx.Option.UnknownOptionException>(msg.Exception);
		}

		[Fact]
		public void Exception_Thrown_Calls_Handler()
		{
			// Arrange
			var option = Option.Wrap(F.Rnd.Str);
			var handler = Substitute.For<Option.Handler>();
			var exception = new Exception();

			// Act
			var result = option.DoMap<int>(_ => throw exception, handler);

			// Assert
			Assert.IsType<None<int>>(result);
			handler.Received().Invoke(exception);
		}

		[Fact]
		public void If_None_Gets_None()
		{
			// Arrange
			var option = Option.None<int>(true);
			var map = Substitute.For<Func<int, string>>();

			// Act
			var r0 = option.DoMap(map, null);
			var r1 = option.Map(map, null);

			// Assert
			Assert.IsType<None<string>>(r0);
			Assert.IsType<None<string>>(r1);
		}

		[Fact]
		public void If_None_With_Reason_Gets_None_With_Same_Reason()
		{
			// Arrange
			var msg = new TestMsg();
			var option = Option.None<int>(msg);
			var map = Substitute.For<Func<int, string>>();

			// Act
			var r0 = option.DoMap(map, null);
			var r1 = option.Map(map, null);

			// Assert
			var n0 = Assert.IsType<None<string>>(r0);
			Assert.Same(msg, n0.Reason);
			var n1 = Assert.IsType<None<string>>(r1);
			Assert.Same(msg, n1.Reason);
		}

		[Fact]
		public void If_Some_Runs_Map_Function()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Option.Wrap(value);
			var map = Substitute.For<Func<int, string>>();

			// Act
			option.DoMap(map, null);
			option.Map(map, null);
			Option.Map(() => map(value), null);

			// Assert
			map.Received(3).Invoke(value);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}

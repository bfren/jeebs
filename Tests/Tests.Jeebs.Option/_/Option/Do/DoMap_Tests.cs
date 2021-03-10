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
		public void If_Unknown_Option_Return_None_With_UnhandledExceptionMsg()
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
		public void If_Some_Run_Map()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Option.Wrap(value);
			var map = Substitute.For<Func<int, string>>();

			// Act
			option.DoMap(map, null);
			option.Map(() => map(value), null);
			option.Map(map, null);

			// Assert
			map.Received(3).Invoke(value);
		}

		public class FakeOption : Option<int> { }
	}
}

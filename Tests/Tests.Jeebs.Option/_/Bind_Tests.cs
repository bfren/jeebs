// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Option.Exceptions;
using JeebsF.OptionFMsg;
using NSubstitute;
using Xunit;
using static JeebsF.OptionF;

namespace Jeebs.Option_Tests
{
	public class Bind_Tests
	{
		[Fact]
		public void If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = new FakeOption();
			var bind = Substitute.For<Func<int, Option<string>>>();

			// Act
			var result = option.DoBind(bind, null);

			// Assert
			var none = Assert.IsType<None<string>>(result);
			var msg = Assert.IsType<UnhandledExceptionMsg>(none.Reason);
			Assert.IsType<UnknownOptionException>(msg.Exception);
		}

		[Fact]
		public void Exception_Thrown_Calls_Handler()
		{
			// Arrange
			var option = Return(JeebsF.Rnd.Str);
			var handler = Substitute.For<Handler>();
			var exception = new Exception();

			// Act
			var result = option.DoBind<int>(_ => throw exception, handler);

			// Assert
			Assert.IsType<None<int>>(result);
			handler.Received().Invoke(exception);
		}

		[Fact]
		public void If_None_Gets_None()
		{
			// Arrange
			var option = None<int>(true);
			var bind = Substitute.For<Func<int, Option<string>>>();

			// Act
			var r0 = option.DoBind(bind, null);
			var r1 = option.Bind(bind, null);

			// Assert
			Assert.IsType<None<string>>(r0);
			Assert.IsType<None<string>>(r1);
		}

		[Fact]
		public void If_None_With_Reason_Gets_None_With_Same_Reason()
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var bind = Substitute.For<Func<int, Option<string>>>();

			// Act
			var r0 = option.DoBind(bind, null);
			var r1 = option.Bind(bind, null);

			// Assert
			var n0 = Assert.IsType<None<string>>(r0);
			Assert.Same(msg, n0.Reason);
			var n1 = Assert.IsType<None<string>>(r1);
			Assert.Same(msg, n1.Reason);
		}

		[Fact]
		public void If_Some_Runs_Bind_Function()
		{
			// Arrange
			var value = JeebsF.Rnd.Int;
			var option = Return(value);
			var bind = Substitute.For<Func<int, Option<string>>>();

			// Act
			option.DoBind(bind, null);
			option.Bind(bind, null);

			// Assert
			bind.Received(2).Invoke(value);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}

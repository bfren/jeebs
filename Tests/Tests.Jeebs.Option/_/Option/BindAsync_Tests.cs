// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace Jeebs.Option_Tests
{
	public class BindAsync_Tests
	{
		[Fact]
		public async Task If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = new FakeOption();
			var bind = Substitute.For<Func<int, Task<Option<string>>>>();

			// Act
			var result = await option.BindAsync(bind, null);

			// Assert
			var none = Assert.IsType<None<string>>(result);
			var msg = Assert.IsType<UnhandledExceptionMsg>(none.Reason);
			Assert.IsType<UnknownOptionException>(msg.Exception);
		}

		[Fact]
		public async Task Exception_Thrown_With_Handler_Calls_Handler()
		{
			// Arrange
			var option = Return(F.Rnd.Str);
			var handler = Substitute.For<Handler>();
			var exception = new Exception();

			// Act
			var r0 = await option.BindAsync<int>(_ => throw exception, handler);

			// Assert
			Assert.IsType<None<int>>(r0);
			handler.Received(1).Invoke(exception);
		}

		[Fact]
		public async Task If_None_Gets_None()
		{
			// Arrange
			var option = None<int>(true);
			var bind = Substitute.For<Func<int, Task<Option<string>>>>();

			// Act
			var r0 = await option.BindAsync(bind);

			// Assert
			Assert.IsType<None<string>>(r0);
		}

		[Fact]
		public async Task If_None_With_Reason_Gets_None_With_Same_Reason()
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var bind = Substitute.For<Func<int, Task<Option<string>>>>();

			// Act
			var r0 = await option.BindAsync(bind, null);

			// Assert
			var n0 = Assert.IsType<None<string>>(r0);
			Assert.Same(msg, n0.Reason);
		}

		[Fact]
		public async Task If_Some_Runs_Bind_Function()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var bind = Substitute.For<Func<int, Task<Option<string>>>>();

			// Act
			await option.BindAsync(bind, null);

			// Assert
			await bind.Received(1).Invoke(value);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}

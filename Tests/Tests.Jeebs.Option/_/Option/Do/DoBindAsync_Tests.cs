// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Option_Tests
{
	public class DoBindAsync_Tests
	{
		[Fact]
		public async Task If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = new FakeOption();
			var some = Substitute.For<Func<int, Task<Option<string>>>>();

			// Act
			var result = await option.DoBindAsync(some, null);

			// Assert
			var none = Assert.IsType<None<string>>(result);
			var msg = Assert.IsType<Jm.Option.UnhandledExceptionMsg>(none.Reason);
			Assert.IsType<Jx.Option.UnknownOptionException>(msg.Exception);
		}

		[Fact]
		public async Task Exception_Thrown_Calls_Handler()
		{
			// Arrange
			var option = Option.Wrap(F.Rnd.Str);
			var handler = Substitute.For<Option.Handler>();
			var exception = new Exception();

			// Act
			var result = await option.DoBindAsync<int>(_ => throw exception, handler);

			// Assert
			Assert.IsType<None<int>>(result);
			handler.Received().Invoke(exception);
		}

		[Fact]
		public async Task If_None_Gets_None()
		{
			// Arrange
			var option = Option.None<int>(true);
			var bind = Substitute.For<Func<int, Task<Option<string>>>>();

			// Act
			var r0 = await option.DoBindAsync(bind, null);
			var r1 = await option.BindAsync(bind, null);

			// Assert
			Assert.IsType<None<string>>(r0);
			Assert.IsType<None<string>>(r1);
		}

		[Fact]
		public async Task If_None_With_Reason_Gets_None_With_Same_Reason()
		{
			// Arrange
			var msg = new TestMsg();
			var option = Option.None<int>(msg);
			var bind = Substitute.For<Func<int, Task<Option<string>>>>();

			// Act
			var r0 = await option.DoBindAsync(bind, null);
			var r1 = await option.BindAsync(bind, null);

			// Assert
			var n0 = Assert.IsType<None<string>>(r0);
			Assert.Same(msg, n0.Reason);
			var n1 = Assert.IsType<None<string>>(r1);
			Assert.Same(msg, n1.Reason);
		}

		[Fact]
		public async Task If_Some_Runs_Bind_Function()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Option.Wrap(value);
			var bind = Substitute.For<Func<int, Task<Option<string>>>>();

			// Act
			await option.DoBindAsync(bind, null);
			await option.BindAsync(bind, null);
			await Option.BindAsync(() => bind(value + 1), null);

			// Assert
			await bind.Received(2).Invoke(value);
			await bind.Received(1).Invoke(value + 1);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}

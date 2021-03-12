// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using F.OptionFMsg;
using Jeebs.Option.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs.OptionExtensions_Tests
{
	public class MapAsync_Tests
	{
		[Fact]
		public async Task If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = new FakeOption();
			var task = option.AsTask;
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			var result = await OptionExtensions.DoMapAsync(task, map, null);

			// Assert
			var none = Assert.IsType<None<string>>(result);
			var msg = Assert.IsType<UnhandledExceptionMsg>(none.Reason);
			Assert.IsType<UnknownOptionException>(msg.Exception);
		}

		[Fact]
		public async Task Exception_Thrown_Calls_Handler()
		{
			// Arrange
			var option = Return(F.Rnd.Int);
			var task = option.AsTask;
			var handler = Substitute.For<Handler>();
			var exception = new Exception();

			string syncThrow(int _) => throw exception;
			Task<string> asyncThrow(int _) => throw exception;

			// Act
			var r0 = await OptionExtensions.DoMapAsync(task, asyncThrow, handler);
			var r1 = await task.MapAsync(syncThrow, handler);
			var r2 = await task.MapAsync(asyncThrow, handler);

			// Assert
			Assert.IsType<None<string>>(r0);
			Assert.IsType<None<string>>(r1);
			Assert.IsType<None<string>>(r2);
			handler.Received(3).Invoke(exception);
		}

		[Fact]
		public async Task If_None_Gets_None()
		{
			// Arrange
			var option = None<int>(true);
			var task = option.AsTask;
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			var r0 = await OptionExtensions.DoMapAsync(task, map, null);
			var r1 = await task.MapAsync(v => map(v).GetAwaiter().GetResult(), null);
			var r2 = await task.MapAsync(map, null);

			// Assert
			Assert.IsType<None<string>>(r0);
			Assert.IsType<None<string>>(r1);
			Assert.IsType<None<string>>(r2);
		}

		[Fact]
		public async Task If_None_With_Reason_Gets_None_With_Same_Reason()
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var task = option.AsTask;
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			var r0 = await OptionExtensions.DoMapAsync(task, map, null);
			var r1 = await task.MapAsync(v => map(v).GetAwaiter().GetResult(), null);
			var r2 = await task.MapAsync(map, null);

			// Assert
			var n0 = Assert.IsType<None<string>>(r0);
			Assert.Same(msg, n0.Reason);
			var n1 = Assert.IsType<None<string>>(r1);
			Assert.Same(msg, n1.Reason);
			var n2 = Assert.IsType<None<string>>(r2);
			Assert.Same(msg, n2.Reason);
		}

		[Fact]
		public async Task If_Some_Runs_Map_Function()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Return(value);
			var task = option.AsTask;
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			await OptionExtensions.DoMapAsync(task, map, null);
			await task.MapAsync(v => map(v).GetAwaiter().GetResult(), null);
			await task.MapAsync(map, null);

			// Assert
			await map.Received(3).Invoke(value);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}

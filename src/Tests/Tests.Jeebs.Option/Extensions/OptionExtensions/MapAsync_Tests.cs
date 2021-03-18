// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs.OptionExtensions_Tests
{
	public class MapAsync_Tests
	{
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
			var r0 = await task.MapAsync(syncThrow, handler);
			var r1 = await task.MapAsync(asyncThrow, handler);

			// Assert
			r0.AssertNone();
			r1.AssertNone();
			handler.Received(2).Invoke(exception);
		}

		[Fact]
		public async Task If_None_Gets_None()
		{
			// Arrange
			var option = None<int>(true);
			var task = option.AsTask;
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			var r0 = await task.MapAsync(v => map(v).GetAwaiter().GetResult(), DefaultHandler);
			var r1 = await task.MapAsync(map, DefaultHandler);

			// Assert
			r0.AssertNone();
			r1.AssertNone();
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
			var r0 = await task.MapAsync(v => map(v).GetAwaiter().GetResult(), DefaultHandler);
			var r1 = await task.MapAsync(map, DefaultHandler);

			// Assert
			var n0 = r0.AssertNone();
			Assert.Same(msg, n0);
			var n1 = r1.AssertNone();
			Assert.Same(msg, n1);
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
			await task.MapAsync(v => map(v).GetAwaiter().GetResult(), DefaultHandler);
			await task.MapAsync(map, DefaultHandler);

			// Assert
			await map.Received(2).Invoke(value);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}

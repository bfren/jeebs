﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.OptionExtensions_Tests
{
	public class MapAsync_Tests
	{
		[Fact]
		public async Task If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = new FakeOption();
			var task = Task.FromResult(option.AsOption);
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			var result = await OptionExtensions.DoMapAsync(task, map, null);

			// Assert
			var none = Assert.IsType<None<string>>(result);
			var msg = Assert.IsType<Jm.Option.UnhandledExceptionMsg>(none.Reason);
			Assert.IsType<Jx.Option.UnknownOptionException>(msg.Exception);
		}

		[Fact]
		public async Task Exception_Thrown_Calls_Handler()
		{
			// Arrange
			var option = Option.Wrap(F.Rnd.Int);
			var task = Task.FromResult(option);
			var handler = Substitute.For<Option.Handler>();
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
			var option = Option.None<int>(true);
			var task = Task.FromResult(option.AsOption);
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			var r0 = await OptionExtensions.DoMapAsync(task, map, null);
			var r1 = await task.MapAsync(map, null);

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
			var task = Task.FromResult(option.AsOption);
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			var r0 = await OptionExtensions.DoMapAsync(task,map, null);
			var r1 = await task.MapAsync(map, null);

			// Assert
			var n0 = Assert.IsType<None<string>>(r0);
			Assert.Same(msg, n0.Reason);
			var n1 = Assert.IsType<None<string>>(r1);
			Assert.Same(msg, n1.Reason);
		}

		[Fact]
		public async Task If_Some_Runs_Map_Function()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Option.Wrap(value);
			var task = Task.FromResult(option);
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			await OptionExtensions.DoMapAsync(task,map, null);
			await task.MapAsync(map, null);

			// Assert
			await map.Received(2).Invoke(value);
		}

		public class FakeOption : Option<int>
		{
			public Option<int> AsOption => this;
		}

		public record TestMsg : IMsg { }
	}
}

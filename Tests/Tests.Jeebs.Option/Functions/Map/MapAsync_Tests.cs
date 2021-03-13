// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace F.OptionF_Tests
{
	public class MapAsync_Tests
	{
		[Fact]
		public async Task If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = new FakeOption();

			// Act
			var r0 = await MapAsync(option, Substitute.For<Func<int, Task<string>>>(), null);
			var r1 = await MapAsync(option.AsTask, Substitute.For<Func<int, Task<string>>>(), null);

			// Assert
			var n0 = Assert.IsType<None<string>>(r0);
			var m0 = Assert.IsType<UnhandledExceptionMsg>(n0.Reason);
			Assert.IsType<UnknownOptionException>(m0.Exception);
			var n1 = Assert.IsType<None<string>>(r1);
			var m1 = Assert.IsType<UnhandledExceptionMsg>(n1.Reason);
			Assert.IsType<UnknownOptionException>(m1.Exception);
		}

		[Fact]
		public async Task Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = Return(Rnd.Str);
			var exception = new Exception();
			Task<int> throwFunc(string _) => throw exception;

			// Act
			var r0 = await MapAsync(option, throwFunc, null);
			var r1 = await MapAsync(() => throwFunc(Rnd.Str), null);

			// Assert
			var n0 = Assert.IsType<None<int>>(r0);
			Assert.IsType<UnhandledExceptionMsg>(n0.Reason);
			var n1 = Assert.IsType<None<int>>(r1);
		}

		[Fact]
		public async Task Exception_Thrown_With_Handler_Returns_None_Calls_Handler()
		{
			// Arrange
			var option = Return(Rnd.Str);
			var handler = Substitute.For<Handler>();
			var exception = new Exception();
			Task<int> throwFunc(string _) => throw exception;

			// Act
			var r0 = await MapAsync(option, throwFunc, handler);
			var r1 = await MapAsync(option.AsTask, throwFunc, handler);
			var r2 = await MapAsync(() => throwFunc(Rnd.Str), handler);

			// Assert
			Assert.IsType<None<int>>(r0);
			Assert.IsType<None<int>>(r1);
			Assert.IsType<None<int>>(r2);
			handler.Received(3).Invoke(exception);
		}

		[Fact]
		public async Task If_None_Gets_None()
		{
			// Arrange
			var option = None<int>(true);
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			var r0 = await MapAsync(option, map, null);
			var r1 = await MapAsync(option.AsTask, map, null);

			// Assert
			Assert.IsType<None<string>>(r0);
			Assert.IsType<None<string>>(r1);
		}

		[Fact]
		public async Task If_None_With_Reason_Gets_None_With_Same_Reason()
		{
			// Arrange
			var msg = new TestMsg();
			var option = None<int>(msg);
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			var r0 = await MapAsync(option, map, Substitute.For<Handler>());
			var r1 = await MapAsync(option.AsTask, map, null);

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
			var value = Rnd.Int;
			var option = Return(value);
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			await MapAsync(option, map, null);
			await MapAsync(option.AsTask, map, null);
			await MapAsync(() => map(value), null);

			// Assert
			await map.Received(3).Invoke(value);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}

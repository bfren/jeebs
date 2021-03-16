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
			var r0 = await MapAsync(option, Substitute.For<Func<int, Task<string>>>(), DefaultHandler);
			var r1 = await MapAsync(option.AsTask, Substitute.For<Func<int, Task<string>>>(), DefaultHandler);

			// Assert
			var n0 = r0.AssertNone();
			var m0 = Assert.IsType<UnhandledExceptionMsg>(n0);
			Assert.IsType<UnknownOptionException>(m0.Exception);
			var n1 = r1.AssertNone();
			var m1 = Assert.IsType<UnhandledExceptionMsg>(n1);
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
			var r0 = await MapAsync(option, throwFunc, DefaultHandler);

			// Assert
			var n0 = r0.AssertNone();
			Assert.IsType<UnhandledExceptionMsg>(n0);
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
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			var r0 = await MapAsync(option, map, DefaultHandler);
			var r1 = await MapAsync(option.AsTask, map, DefaultHandler);

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
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			var r0 = await MapAsync(option, map, DefaultHandler);
			var r1 = await MapAsync(option.AsTask, map, DefaultHandler);

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
			var value = Rnd.Int;
			var option = Return(value);
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			await MapAsync(option, map, DefaultHandler);
			await MapAsync(option.AsTask, map, DefaultHandler);

			// Assert
			await map.Received(2).Invoke(value);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}

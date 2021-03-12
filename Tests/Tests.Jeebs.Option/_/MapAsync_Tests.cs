// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using F.OptionFMsg;
using Jeebs.Option.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs.Option_Tests
{
	public class MapAsync_Tests
	{
		[Fact]
		public async Task If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = new FakeOption();
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			var result = await option.DoMapAsync(map, null);

			// Assert
			var none = Assert.IsType<None<string>>(result);
			var msg = Assert.IsType<UnhandledExceptionMsg>(none.Reason);
			Assert.IsType<UnknownOptionException>(msg.Exception);
		}

		[Fact]
		public async Task Exception_Thrown_Calls_Handler()
		{
			// Arrange
			var option = Return(F.Rnd.Str);
			var handler = Substitute.For<Handler>();
			var exception = new Exception();

			// Act
			var result = await option.DoMapAsync<int>(_ => throw exception, handler);

			// Assert
			Assert.IsType<None<int>>(result);
			handler.Received().Invoke(exception);
		}

		[Fact]
		public async Task If_None_Gets_None()
		{
			// Arrange
			var option = None<int>(true);
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			var r0 = await option.DoMapAsync(map, null);
			var r1 = await option.MapAsync(map, null);

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
			var r0 = await option.DoMapAsync(map, null);
			var r1 = await option.MapAsync(map, null);

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
			var option = Return(value);
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			await option.DoMapAsync(map, null);
			await option.MapAsync(map, null);

			// Assert
			await map.Received(2).Invoke(value);
		}

		public class FakeOption : Option<int> { }

		public record TestMsg : IMsg { }
	}
}

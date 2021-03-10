// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Option_Tests
{
	public class DoMapAsync_Tests
	{
		[Fact]
		public async Task If_Unknown_Option_Return_None_With_UnhandledExceptionMsg()
		{
			// Arrange
			var option = new FakeOption();
			var some = Substitute.For<Func<int, Task<string>>>();

			// Act
			var result = await option.DoMapAsync(some, null);

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
			var result = await option.DoMapAsync<int>(_ => throw exception, handler);

			// Assert
			Assert.IsType<None<int>>(result);
			handler.Received().Invoke(exception);
		}

		[Fact]
		public async Task If_Some_Run_Map()
		{
			// Arrange
			var value = F.Rnd.Int;
			var option = Option.Wrap(value);
			var map = Substitute.For<Func<int, Task<string>>>();

			// Act
			await option.DoMapAsync(map, null);
			await option.MapAsync(() => map(value + 1), null);
			await option.MapAsync(map, null);

			// Assert
			await map.Received(2).Invoke(value);
			await map.Received(1).Invoke(value + 1);
		}

		public class FakeOption : Option<int> { }
	}
}

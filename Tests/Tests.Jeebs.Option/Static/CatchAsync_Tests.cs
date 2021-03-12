// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace JeebsF.OptionStatic_Tests
{
	public class CatchAsync_Tests
	{
		[Fact]
		public async Task Executes_Chain()
		{
			// Arrange
			var value = JeebsF.Rnd.Int;

			// Act
			var result = await OptionF.CatchAsync(() => Task.FromResult(OptionF.Return(value)));

			// Assert
			var some = Assert.IsType<Some<int>>(result);
			Assert.Equal(value, some.Value);
		}

		[Fact]
		public async Task Catches_Exception_Without_Handler()
		{
			// Arrange
			var message = JeebsF.Rnd.Str;

			// Act
			var result = await OptionF.CatchAsync<int>(() => throw new Exception(message));

			// Assert
			var none = Assert.IsType<None<int>>(result);
			var ex = Assert.IsType<Jm.Option.UnhandledExceptionMsg>(none.Reason);
			Assert.Contains(message, ex.ToString());
		}

		[Fact]
		public async Task Catches_Exception_With_Handler()
		{
			// Arrange
			var message = JeebsF.Rnd.Str;
			var exception = new Exception(message);
			var handler = Substitute.For<OptionF.Handler>();

			// Act
			var result = await OptionF.CatchAsync<int>(() => throw exception, handler);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			handler.Received().Invoke(exception);
		}
	}
}

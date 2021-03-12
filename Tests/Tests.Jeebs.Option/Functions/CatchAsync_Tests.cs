// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using F.OptionFMsg;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class CatchAsync_Tests
	{
		[Fact]
		public async Task Executes_Chain()
		{
			// Arrange
			var value = Rnd.Int;

			// Act
			var result = await CatchAsync(() => Return(value).AsTask);

			// Assert
			var some = Assert.IsType<Some<int>>(result);
			Assert.Equal(value, some.Value);
		}

		[Fact]
		public async Task Catches_Exception_Without_Handler()
		{
			// Arrange
			var message = Rnd.Str;

			// Act
			var result = await CatchAsync<int>(() => throw new Exception(message));

			// Assert
			var none = Assert.IsType<None<int>>(result);
			var ex = Assert.IsType<UnhandledExceptionMsg>(none.Reason);
			Assert.Contains(message, ex.ToString());
		}

		[Fact]
		public async Task Catches_Exception_With_Handler()
		{
			// Arrange
			var message = Rnd.Str;
			var exception = new Exception(message);
			var handler = Substitute.For<Handler>();

			// Act
			var result = await CatchAsync<int>(() => throw exception, handler);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			handler.Received().Invoke(exception);
		}
	}
}

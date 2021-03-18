// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

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
			var result = await CatchAsync(() => Return(value).AsTask, DefaultHandler);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(value, some);
		}

		[Fact]
		public async Task Catches_Exception_Without_Handler()
		{
			// Arrange
			var message = Rnd.Str;

			// Act
			var result = await CatchAsync<int>(() => throw new Exception(message), DefaultHandler);

			// Assert
			var none = result.AssertNone();
			var ex = Assert.IsType<UnhandledExceptionMsg>(none);
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
			var none = result.AssertNone();
			handler.Received().Invoke(exception);
		}
	}
}

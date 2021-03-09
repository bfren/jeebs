// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using NSubstitute;
using Xunit;

namespace Jeebs.Option_Static_Tests
{
	public class Catch_Tests
	{
		[Fact]
		public void Executes_Chain()
		{
			// Arrange
			var value = F.Rnd.Int;

			// Act
			var result = Option.Catch(() => Option.Wrap(value));

			// Assert
			var some = Assert.IsType<Some<int>>(result);
			Assert.Equal(value, some.Value);
		}

		[Fact]
		public void Catches_Exception_Without_Handler()
		{
			// Arrange
			var message = F.Rnd.Str;

			// Act
			var result = Option.Catch<int>(() => throw new Exception(message));

			// Assert
			var none = Assert.IsType<None<int>>(result);
			var ex = Assert.IsType<Jm.Option.UnhandledExceptionMsg>(none.Reason);
			Assert.Contains(message, ex.ToString());
		}

		[Fact]
		public void Catches_Exception_With_Handler()
		{
			// Arrange
			var message = F.Rnd.Str;
			var exception = new Exception(message);
			var handler = Substitute.For<Func<Exception, IExceptionMsg>>();

			// Act
			var result = Option.Catch<int>(() => throw exception, handler);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			handler.Received().Invoke(exception);
		}
	}
}

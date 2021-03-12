// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using JeebsF.OptionFMsg;
using NSubstitute;
using Xunit;
using static JeebsF.OptionF;

namespace JeebsF.OptionF_Tests
{
	public class Catch_Tests
	{
		[Fact]
		public void Executes_Chain()
		{
			// Arrange
			var value = Rnd.Int;

			// Act
			var result = Catch(() => Return(value));

			// Assert
			var some = Assert.IsType<Some<int>>(result);
			Assert.Equal(value, some.Value);
		}

		[Fact]
		public void Catches_Exception_Without_Handler()
		{
			// Arrange
			var message = Rnd.Str;

			// Act
			var result = Catch<int>(() => throw new Exception(message));

			// Assert
			var none = Assert.IsType<None<int>>(result);
			var ex = Assert.IsType<UnhandledExceptionMsg>(none.Reason);
			Assert.Contains(message, ex.ToString());
		}

		[Fact]
		public void Catches_Exception_With_Handler()
		{
			// Arrange
			var message = Rnd.Str;
			var exception = new Exception(message);
			var handler = Substitute.For<Handler>();

			// Act
			var result = Catch<int>(() => throw exception, handler);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			handler.Received().Invoke(exception);
		}
	}
}

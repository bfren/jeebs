// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Logging;
using Xunit;

namespace Jeebs.Abstractions.Messages.LogMsg_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Default_Level_Is_Error()
		{
			// Arrange
			var msg = new TestMsg0();

			// Act
			var result = msg.Level;

			// Assert
			Assert.Equal(LogLevel.Error, result);
		}

		[Fact]
		public void Parameterless_Creates_Unknown_Exception()
		{
			// Arrange
			var msg = new TestMsg0();

			// Act
			var result = msg.Exception.Message;

			// Assert
			Assert.Equal("Unknown.", result);
		}

		[Fact]
		public void With_Exception_Sets_Exception()
		{
			// Arrange
			var value = F.Rnd.Str;
			var ex = new Exception(value);
			var msg = new TestMsg1(ex);

			// Act
			var result = msg.Exception;

			// Assert
			Assert.Same(ex, result);
			Assert.Equal(value, result.Message);
		}

		public record TestMsg0 : ExceptionMsg { }

		public record TestMsg1 : ExceptionMsg
		{
			public TestMsg1(Exception e) : base(e) { }
		}
	}
}

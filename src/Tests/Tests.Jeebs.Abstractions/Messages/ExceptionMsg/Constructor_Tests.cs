// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Logging;
using Xunit;

namespace Jeebs.ExceptionMsg_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Parameterless_Creates_Default_Values()
		{
			// Arrange

			// Act
			var msg = new TestMsg();

			// Assert
			Assert.Equal("Unknown.", msg.Exception.Message);
			Assert.Equal("{MsgType} {Exception}", msg.Format);
			Assert.Equal(LogLevel.Error, msg.Level);
		}

		[Fact]
		public void With_Exception_Sets_Exception()
		{
			// Arrange
			var value = new Exception(F.Rnd.Str);

			// Act
			var msg = new TestMsg(value);

			// Assert
			Assert.Equal(value, msg.Exception);
		}

		[Fact]
		public void Appends_Exception_To_Format()
		{
			// Arrange
			var value = F.Rnd.Str;

			// Act
			var result = new TestMsg(new Exception(), value);

			// Assert
			Assert.Equal("{MsgType} " + value + " {Exception}", result.Format);
		}

		[Fact]
		public void With_Level_Sets_Level()
		{
			// Arrange
			const LogLevel value = LogLevel.Warning;

			// Act
			var result = new TestMsg(value, new Exception(), F.Rnd.Str);

			// Assert
			Assert.Equal(value, result.Level);
		}

		public record TestMsg : ExceptionMsg
		{
			public TestMsg() { }
			public TestMsg(Exception ex) : base(ex) { }
			public TestMsg(Exception ex, string format) : base(ex, format) { }
			public TestMsg(LogLevel level, Exception ex, string format) : base(level, ex, format) { }
		}
	}
}

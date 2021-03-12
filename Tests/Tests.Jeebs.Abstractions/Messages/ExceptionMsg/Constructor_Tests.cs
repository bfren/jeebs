// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Logging;
using Xunit;

namespace Jeebs.Abstractions.Messages.ExceptionMsg_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Parameterless_Sets_Level_To_Information()
		{
			// Arrange
			var msg = new TestMsg0();

			// Act
			var result = msg.Level;

			// Assert
			Assert.Equal(LogLevel.Information, result);
		}

		[Fact]
		public void With_Level_Sets_Level()
		{
			// Arrange
			const LogLevel value = LogLevel.Fatal;
			var msg = new TestMsg1(value);

			// Act
			var result = msg.Level;

			// Assert
			Assert.Equal(value, result);
		}

		public record TestMsg0 : LogMsg { }

		public record TestMsg1 : LogMsg
		{
			public TestMsg1(LogLevel level) : base(level) { }
		}
	}
}

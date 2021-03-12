// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Logging;
using Xunit;

namespace Jeebs.Abstractions.Messages.LogMsg_Tests
{
	public class ToString_Tests
	{
		[Fact]
		public void Returns_LogLevel()
		{
			// Arrange
			const LogLevel value = LogLevel.Verbose;
			var msg = new TestMsg(value);

			// Act
			var result = msg.ToString();

			// Assert
			Assert.Contains(value.ToString(), result);
			Assert.Equal($"TestMsg {{ Level = {value} }}", result);
		}

		public record TestMsg : LogMsg
		{
			public TestMsg(LogLevel level) : base(level) { }
		}
	}
}

// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Logging;

namespace Jeebs.Messages.ExceptionMsg_Tests;

public class Level_Tests
{
	[Fact]
	public void Returns_Error()
	{
		// Arrange
		var msg = new TestMsg(new());

		// Act
		var result = msg.Level;

		// Assert
		Assert.Equal(LogLevel.Error, result);
	}

	public sealed record class TestMsg(Exception Value) : ExceptionMsg;
}

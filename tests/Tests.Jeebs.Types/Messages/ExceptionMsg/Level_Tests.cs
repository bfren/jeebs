// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs.Logging;
using Xunit;

namespace Jeebs.ExceptionMsg_Tests;

public class Level_Tests
{
	[Fact]
	public void Uses_LogLevel_Error()
	{
		// Arrange
		var msg = new TestExceptionMsg(new());

		// Act
		var result = msg.Level;

		// Assert
		Assert.Equal(LogLevel.Error, result);
	}

	public record class TestExceptionMsg(Exception Value) : ExceptionMsg;
}

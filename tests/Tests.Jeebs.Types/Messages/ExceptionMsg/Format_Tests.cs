// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Xunit;

namespace Jeebs.ExceptionMsg_Tests;

public class Format_Tests
{
	[Fact]
	public void Uses_Exception_As_Name()
	{
		// Arrange
		var msg = new TestExceptionMsg(new());

		// Act
		var result = msg.Format;

		// Assert
		Assert.Equal("{{ Exception = {Value} }}", result);
	}

	public record class TestExceptionMsg(Exception Value) : ExceptionMsg;
}

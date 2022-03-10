// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Messages.ExceptionMsg_Tests;

public class Name_Tests
{
	[Fact]
	public void Returns_Exception()
	{
		// Arrange
		var msg = new TestMsg(new());

		// Act
		var result = msg.Name;

		// Assert
		Assert.Equal(nameof(Exception), result);
	}

	public sealed record class TestMsg(Exception Value) : ExceptionMsg;
}

// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Messages.Msg_Tests;

public class Level_Tests
{
	[Fact]
	public void Returns_DefaultLevel()
	{
		// Arrange
		var msg = new TestMsg();

		// Act
		var result = msg.Level;

		// Assert
		Assert.Equal(Msg.DefaultLevel, result);
	}

	public sealed record class TestMsg : Msg;
}

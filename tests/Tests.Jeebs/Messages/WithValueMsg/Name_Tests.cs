// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Messages.WithValueMsg_Tests;

public class Name_Tests
{
	[Fact]
	public void Returns_Value()
	{
		// Arrange
		var msg = new TestMsg(Rnd.Str);

		// Act
		var result = msg.Name;

		// Assert
		Assert.Equal(nameof(TestMsg.Value), result);
	}

	public sealed record class TestMsg(string Value) : WithValueMsg<string>;
}

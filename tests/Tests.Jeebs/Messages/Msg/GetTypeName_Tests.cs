// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Messages.Msg_Tests;

public class GetTypeName_Tests
{
	[Fact]
	public void Returns_Type_String()
	{
		// Arrange
		var msg = new TestMsg();

		// Act
		var result = msg.GetTypeName();

		// Assert
		Assert.Equal(typeof(TestMsg).ToString(), result);
	}

	public sealed record class TestMsg : Msg;
}

// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Msg_Tests;

public class GetTypeName_Tests
{
	[Fact]
	public void Returns_Type_Name()
	{
		// Arrange
		var msg = new TestMsg();

		// Act
		var result = msg.GetTypeName();

		// Assert
		Assert.Equal(typeof(TestMsg).ToString(), result);
	}

	public record class TestMsg : Msg;
}

// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Msg_Tests;

public class ArgsWithType_Tests
{
	[Fact]
	public void Adds_Msg_Type()
	{
		// Arrange
		var msg = new TestMsg();

		// Act
		var result = msg.ArgsWithType;

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(msg.GetTypeName(), x)
		);
	}

	[Fact]
	public void Inserts_Msg_Type()
	{
		// Arrange
		var v0 = F.Rnd.Int;
		var v1 = F.Rnd.Lng;
		var args = new object[] { v0, v1 };
		var msg = new TestMsg(args);

		// Act
		var result = msg.ArgsWithType;

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(msg.GetTypeName(), x),
			x => Assert.Equal(v0, x),
			x => Assert.Equal(v1, x)
		);
	}

	public record class TestMsg : Msg
	{
		public TestMsg() { }

		public TestMsg(object[] args) =>
			Args = args;
	}
}

// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Messages.Msg_Tests;

public class ArgsWithType_Tests
{
	[Fact]
	public void Returns_TypeName_When_Null()
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
	public void Prepends_TypeName_To_Args()
	{
		// Arrange
		var a0 = Rnd.Str;
		var a1 = Rnd.Guid;
		var args = new object[] { a0, a1 };
		var msg = new TestMsg(args);

		// Act
		var result = msg.ArgsWithType;

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(msg.GetTypeName(), x),
			x => Assert.Same(a0, x),
			x => Assert.Equal(a1, x)
		);
	}

	public sealed record class TestMsg : Msg
	{
		public TestMsg() { }

		public TestMsg(object[] args) =>
			Args = args;
	}
}

// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Messages.WithValueMsg;

public class Args_Tests
{
	[Fact]
	public void Returns_Empty_Object_When_Value_Is_Null()
	{
		// Arrange
		var msg = new TestMsg(null);

		// Act
		var result = msg.Args;

		// Assert
		Assert.Collection(result!,
			x => Assert.NotNull(x)
		);
	}

	[Fact]
	public void Returns_Value()
	{
		// Arrange
		var value = Rnd.Str;
		var msg = new TestMsg(value);

		// Act
		var result = msg.Args;

		// Assert
		Assert.Collection(result!,
			x => Assert.Equal(value, x)
		);
	}

	public sealed record class TestMsg(string? Value) : WithValueMsg<string?>;
}

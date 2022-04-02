// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Messages.WithValueMsg;

public class Format_Tests
{
	[Fact]
	public void Uses_Name()
	{
		// Arrange
		var name = Rnd.Str;
		var msg = new TestMsg(Rnd.Int) { Name = name };
		var expected = $"{name} = {{Value}}";

		// Act
		var result = msg.Format;

		// Assert
		Assert.Equal(expected, result);
	}

	public sealed record class TestMsg(int Value) : WithValueMsg<int>;
}

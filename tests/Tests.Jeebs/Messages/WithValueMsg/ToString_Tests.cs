// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Messages.WithValueMsg_Tests;

public class ToString_Tests
{
	[Fact]
	public void Returns_Name_And_Value()
	{
		// Arrange
		var name = Rnd.Str;
		var value = Rnd.Int;
		var msg = new TestMsg(value) { Name = name };
		var expected = $"{typeof(TestMsg)} {name} = {value}";

		// Act
		var result = msg.ToString();

		// Assert
		Assert.Equal(expected, result);
	}

	public sealed record class TestMsg(int Value) : WithValueMsg<int>;
}

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
		var msg = new TestMsg(name, value);
		var expected = $"{typeof(TestMsg)} {name} = {value}";

		// Act
		var result = msg.ToString();

		// Assert
		Assert.Equal(expected, result);
	}

	public sealed record class TestMsg : WithValueMsg<int>
	{
		public override int Value { get; init; }

		public TestMsg(string name, int value) : base(name) =>
			Value = value;
	}
}

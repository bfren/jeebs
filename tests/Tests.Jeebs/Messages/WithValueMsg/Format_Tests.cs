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
		var msg = new TestMsg(name, Rnd.Int);
		var expected = $"{name} = {{Value}}";

		// Act
		var result = msg.Format;

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

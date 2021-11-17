// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.WithValueMsg_Tests;

public class Args_Tests
{
	[Fact]
	public void Includes_Value()
	{
		// Arrange
		var value = F.Rnd.Str;
		var msg = new TestMsg(value);

		// Act
		var result = msg.Args;

		// Assert
		Assert.Collection(result,
			x => Assert.Equal(value, x)
		);
	}

	public record class TestMsg(string Value) : WithValueMsg<string>;
}

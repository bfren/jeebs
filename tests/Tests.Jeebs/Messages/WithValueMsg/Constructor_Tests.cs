// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using MaybeF;

namespace Jeebs.Messages.WithValueMsg_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Implements_IMsg()
	{
		// Arrange
		var value = Rnd.Str;

		// Act
		var result = new TestMsg(value);

		// Assert
		Assert.IsAssignableFrom<IMsg>(result);
	}

	[Fact]
	public void Implements_IWithValueMsg()
	{
		// Arrange
		var value = Rnd.Str;

		// Act
		var result = new TestMsg(value);

		// Assert
		Assert.IsAssignableFrom<IWithValueMsg<string>>(result);
	}

	public sealed record class TestMsg(string Value) : WithValueMsg<string>;
}

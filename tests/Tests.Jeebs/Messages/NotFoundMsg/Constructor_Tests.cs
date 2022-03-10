// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Messages.NotFoundMsg_Tests;

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
	public void Implements_INotFoundMsg()
	{
		// Arrange
		var value = Rnd.Str;

		// Act
		var result = new TestMsg(value);

		// Assert
		Assert.IsAssignableFrom<INotFoundMsg>(result);
	}

	public sealed record class TestMsg(string Value) : NotFoundMsg<string>;
}

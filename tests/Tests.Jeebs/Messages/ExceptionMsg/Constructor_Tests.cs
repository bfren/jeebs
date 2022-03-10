// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Maybe;

namespace Jeebs.Messages.ExceptionMsg_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Implements_IExceptionReason()
	{
		// Arrange

		// Act
		var result = new TestMsg(new());

		// Assert
		Assert.IsAssignableFrom<IExceptionReason>(result);
	}

	[Fact]
	public void Implements_IMsg()
	{
		// Arrange

		// Act
		var result = new TestMsg(new());

		// Assert
		Assert.IsAssignableFrom<IMsg>(result);
	}

	[Fact]
	public void Implements_IExceptionMsg()
	{
		// Arrange

		// Act
		var result = new TestMsg(new());

		// Assert
		Assert.IsAssignableFrom<IExceptionMsg>(result);
	}

	public sealed record class TestMsg(Exception Value) : ExceptionMsg;
}

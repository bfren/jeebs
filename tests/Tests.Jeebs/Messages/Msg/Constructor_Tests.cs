﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Maybe;

namespace Jeebs.Messages.Msg_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Implements_IReason()
	{
		// Arrange

		// Act
		var result = new TestMsg();

		// Assert
		Assert.IsAssignableFrom<IReason>(result);
	}

	public sealed record class TestMsg : Msg;
}

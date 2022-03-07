// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Xunit;
using static F.MaybeF;

namespace Jeebs.Internals.None_Tests;

public class ToString_Tests
{
	[Fact]
	public void When_Not_ExceptionMsg_Returns_Reason_ToString()
	{
		// Arrange
		var msg = new TestMsg();
		var maybe = None<int>(msg);

		// Act
		var result = maybe.ToString();

		// Assert
		Assert.Equal($"{typeof(TestMsg)}", result);
	}

	[Fact]
	public void When_ExceptionMsg_Returns_Msg_Type_And_Exception_Message()
	{
		// Arrange
		var value = F.Rnd.Str;
		var exception = new Exception(value);
		var maybe = None<int, TestExceptionMsg>(exception);

		// Act
		var result = maybe.ToString();

		// Assert
		Assert.Equal($"{typeof(TestExceptionMsg)}: {value}", result);
	}

	public record class TestMsg : Msg;

	public record class TestExceptionMsg(Exception Value) : ExceptionMsg;
}

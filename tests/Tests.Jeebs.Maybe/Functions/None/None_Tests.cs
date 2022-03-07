// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.MaybeF;

namespace F.MaybeF_Tests;

public class None_Tests
{
	[Fact]
	public void Returns_None_Without_Reason()
	{
		// Arrange

		// Act
		var result = Create.None<int>();

		// Assert
		_ = result.AssertNone();
	}

	[Fact]
	public void Returns_None_With_Reason_Object()
	{
		// Arrange
		var reason = Substitute.For<Msg>();

		// Act
		var result = None<int>(reason);

		// Assert
		var none = result.AssertNone();
		Assert.Same(reason, none);
	}

	[Fact]
	public void Returns_None_With_Reason_Type()
	{
		// Arrange

		// Act
		var result = None<int, TestMsg>();

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<TestMsg>(none);
	}

	[Fact]
	public void Returns_None_With_Reason_Exception_Type()
	{
		// Arrange
		var exception = new Exception();

		// Act
		var result = None<int, TestExceptionMsg>(exception);

		// Assert
		var none = result.AssertNone();
		var msg = Assert.IsType<TestExceptionMsg>(none);
		Assert.Same(exception, msg.Value);
	}

	public record class TestMsg : Msg;

	public record class TestExceptionMsg(Exception Value) : ExceptionMsg;
}

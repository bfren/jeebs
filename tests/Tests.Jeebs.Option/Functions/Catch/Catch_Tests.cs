// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.M;

namespace F.OptionF_Tests;

public class Catch_Tests
{
	[Fact]
	public void Executes_Chain()
	{
		// Arrange
		var value = Rnd.Int;

		// Act
		var result = Catch(() => Some(value), DefaultHandler);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}

	[Fact]
	public void Catches_Exception_Without_Handler()
	{
		// Arrange
		var message = Rnd.Str;

		// Act
		var result = Catch<int>(() => throw new Exception(message), DefaultHandler);

		// Assert
		var none = result.AssertNone();
		var ex = Assert.IsType<UnhandledExceptionMsg>(none);
		Assert.Contains(message, ex.ToString());
	}

	[Fact]
	public void Catches_Exception_With_Handler()
	{
		// Arrange
		var message = Rnd.Str;
		var exception = new Exception(message);
		var handler = Substitute.For<Handler>();

		// Act
		var result = Catch<int>(() => throw exception, handler);

		// Assert
		var none = result.AssertNone();
		handler.Received().Invoke(exception);
	}
}

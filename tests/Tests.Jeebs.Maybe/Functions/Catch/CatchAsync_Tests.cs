﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.MaybeF;
using static F.MaybeF.M;

namespace F.MaybeF_Tests;

public class CatchAsync_Tests
{
	[Fact]
	public async Task Executes_Chain()
	{
		// Arrange
		var value = Rnd.Int;

		// Act
		var result = await CatchAsync(() => Some(value).AsTask, DefaultHandler).ConfigureAwait(false);

		// Assert
		var some = result.AssertSome();
		Assert.Equal(value, some);
	}

	[Fact]
	public async Task Catches_Exception_Without_Handler()
	{
		// Arrange
		var message = Rnd.Str;

		// Act
		var result = await CatchAsync<int>(() => throw new Exception(message), DefaultHandler).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		var ex = Assert.IsType<UnhandledExceptionMsg>(none);
		Assert.Contains(message, ex.ToString());
	}

	[Fact]
	public async Task Catches_Exception_With_Handler()
	{
		// Arrange
		var message = Rnd.Str;
		var exception = new Exception(message);
		var handler = Substitute.For<Handler>();

		// Act
		var result = await CatchAsync<int>(() => throw exception, handler).ConfigureAwait(false);

		// Assert
		_ = result.AssertNone();
		handler.Received().Invoke(exception);
	}
}
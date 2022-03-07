// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.MaybeF;

namespace Jeebs_Tests;

public abstract class Unwrap_Tests
{
	public abstract void Test00_None_Runs_IfNone_Func_Returns_Value();

	protected static void Test00(Func<Maybe<int>, Func<int>, int> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Create.None<int>();
		var ifNone = Substitute.For<Func<int>>();
		_ = ifNone.Invoke().Returns(value);

		// Act
		var result = act(maybe, ifNone);

		// Assert
		_ = ifNone.Received().Invoke();
		Assert.Equal(value, result);
	}

	public abstract void Test01_None_With_Reason_Runs_IfNone_Func_Passes_Reason_Returns_Value();

	protected static void Test01(Func<Maybe<int>, Func<Msg, int>, int> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var msg = Substitute.ForPartsOf<Msg>();
		var maybe = None<int>(msg);
		var ifNone = Substitute.For<Func<Msg, int>>();
		_ = ifNone.Invoke(msg).Returns(value);

		// Act
		var result = act(maybe, ifNone);

		// Assert
		_ = ifNone.Received().Invoke(msg);
		Assert.Equal(value, result);
	}

	public abstract void Test02_Some_Returns_Value();

	protected static void Test02(Func<Maybe<int>, int> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Some(value);

		// Act
		var result = act(maybe);

		// Assert
		Assert.Equal(value, result);
	}
}

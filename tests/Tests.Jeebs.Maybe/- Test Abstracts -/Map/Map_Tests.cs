// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.MaybeF;
using static F.MaybeF.M;

namespace Jeebs_Tests;

public abstract class Map_Tests
{
	public abstract void Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionMsg();

	protected static void Test00(Func<Maybe<int>, Func<int, string>, Handler, Maybe<string>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();
		var map = Substitute.For<Func<int, string>>();

		// Act
		var result = act(maybe, map, DefaultHandler);

		// Assert
		var none = result.AssertNone();
		var msg = Assert.IsType<UnhandledExceptionMsg>(none);
		Assert.IsType<UnknownMaybeException>(msg.Value);
	}

	public abstract void Test01_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg();

	protected static void Test01(Func<Maybe<string>, Func<string, int>, Handler, Maybe<int>> act)
	{
		// Arrange
		var maybe = Some(F.Rnd.Str);
		var exception = new Exception();
		var throwFunc = int (string _) => throw exception;

		// Act
		var result = act(maybe, throwFunc, DefaultHandler);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<UnhandledExceptionMsg>(none);
	}

	public abstract void Test02_Exception_Thrown_With_Handler_Calls_Handler_Returns_None();

	protected static void Test02(Func<Maybe<string>, Func<string, int>, Handler, Maybe<int>> act)
	{
		// Arrange
		var maybe = Some(F.Rnd.Str);
		var handler = Substitute.For<Handler>();
		var exception = new Exception();
		var throwFunc = int (string _) => throw exception;

		// Act
		var result = act(maybe, throwFunc, handler);

		// Assert
		result.AssertNone();
		handler.Received().Invoke(exception);
	}

	public abstract void Test03_If_None_Returns_None();

	protected static void Test03(Func<Maybe<int>, Func<int, string>, Handler, Maybe<string>> act)
	{
		// Arrange
		var maybe = Create.None<int>();
		var map = Substitute.For<Func<int, string>>();

		// Act
		var result = act(maybe, map, DefaultHandler);

		// Assert
		result.AssertNone();
	}

	public abstract void Test04_If_None_With_Reason_Returns_None_With_Same_Reason();

	protected static void Test04(Func<Maybe<int>, Func<int, string>, Handler, Maybe<string>> act)
	{
		// Arrange
		var msg = new TestMsg();
		var maybe = None<int>(msg);
		var map = Substitute.For<Func<int, string>>();

		// Act
		var result = act(maybe, map, DefaultHandler);

		// Assert
		var none = result.AssertNone();
		Assert.Same(msg, none);
	}

	public abstract void Test05_If_Some_Runs_Map_Function();

	protected static void Test05(Func<Maybe<int>, Func<int, string>, Handler, Maybe<string>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Some(value);
		var map = Substitute.For<Func<int, string>>();

		// Act
		act(maybe, map, DefaultHandler);

		// Assert
		map.Received().Invoke(value);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg : Msg;
}

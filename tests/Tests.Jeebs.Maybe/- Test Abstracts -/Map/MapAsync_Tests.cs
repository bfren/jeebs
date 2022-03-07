// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.MaybeF;
using static F.MaybeF.M;

namespace Jeebs_Tests;

public abstract class MapAsync_Tests
{
	public abstract Task Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionMsg();

	protected static async Task Test00(Func<Maybe<int>, Func<int, Task<string>>, Handler, Task<Maybe<string>>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();
		var map = Substitute.For<Func<int, Task<string>>>();

		// Act
		var result = await act(maybe, map, DefaultHandler).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		var msg = Assert.IsType<UnhandledExceptionMsg>(none);
		_ = Assert.IsType<UnknownMaybeException>(msg.Value);
	}

	public abstract Task Test01_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg();

	protected static async Task Test01(Func<Maybe<string>, Func<string, Task<int>>, Handler, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = Some(F.Rnd.Str);
		var exception = new Exception();
		var throwFunc = Task<int> (string _) => throw exception;

		// Act
		var result = await act(maybe, throwFunc, DefaultHandler).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		_ = Assert.IsType<UnhandledExceptionMsg>(none);
	}

	public abstract Task Test02_Exception_Thrown_With_Handler_Calls_Handler_Returns_None();

	protected static async Task Test02(Func<Maybe<string>, Func<string, Task<int>>, Handler, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = Some(F.Rnd.Str);
		var handler = Substitute.For<Handler>();
		var exception = new Exception();
		var throwFunc = Task<int> (string _) => throw exception;

		// Act
		var result = await act(maybe, throwFunc, handler).ConfigureAwait(false);

		// Assert
		_ = result.AssertNone();
		_ = handler.Received().Invoke(exception);
	}

	public abstract Task Test03_If_None_Returns_None();

	protected static async Task Test03(Func<Maybe<int>, Func<int, Task<string>>, Handler, Task<Maybe<string>>> act)
	{
		// Arrange
		var maybe = Create.None<int>();
		var map = Substitute.For<Func<int, Task<string>>>();

		// Act
		var result = await act(maybe, map, DefaultHandler).ConfigureAwait(false);

		// Assert
		_ = result.AssertNone();
	}

	public abstract Task Test04_If_None_With_Reason_Returns_None_With_Same_Reason();

	protected static async Task Test04(Func<Maybe<int>, Func<int, Task<string>>, Handler, Task<Maybe<string>>> act)
	{
		// Arrange
		var msg = new TestMsg();
		var maybe = None<int>(msg);
		var map = Substitute.For<Func<int, Task<string>>>();

		// Act
		var result = await act(maybe, map, DefaultHandler).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		Assert.Same(msg, none);
	}

	public abstract Task Test05_If_Some_Runs_Map_Function();

	protected static async Task Test05(Func<Maybe<int>, Func<int, Task<string>>, Handler, Task<Maybe<string>>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Some(value);
		var map = Substitute.For<Func<int, Task<string>>>();

		// Act
		_ = await act(maybe, map, DefaultHandler).ConfigureAwait(false);

		// Assert
		_ = await map.Received().Invoke(value).ConfigureAwait(false);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg : Msg;
}

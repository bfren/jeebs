// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.MaybeF;

namespace Jeebs_Tests;

public abstract class SwitchAsync_Tests
{
	public abstract Task Test00_If_Unknown_Maybe_Throws_UnknownOptionException();

	protected static async Task Test00(Func<Maybe<int>, Task<string>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var action = async Task<string> () => await act(maybe).ConfigureAwait(false);

		// Assert
		_ = await Assert.ThrowsAsync<UnknownMaybeException>(action).ConfigureAwait(false);
	}

	public abstract Task Test01_If_None_Runs_None_Func_With_Reason();

	protected static async Task Test01(Func<Maybe<int>, Func<Msg, Task<string>>, Task<string>> act)
	{
		// Arrange
		var reason = new TestMsg();
		var maybe = None<int>(reason);
		var none = Substitute.For<Func<Msg, Task<string>>>();

		// Act
		_ = await act(maybe, none).ConfigureAwait(false);

		// Assert
		_ = await none.Received().Invoke(reason).ConfigureAwait(false);
	}

	public abstract Task Test02_If_Some_Runs_Some_Func_With_Value();

	protected static async Task Test02(Func<Maybe<int>, Func<int, Task<string>>, Task<string>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Some(value);
		var some = Substitute.For<Func<int, Task<string>>>();

		// Act
		_ = await act(maybe, some).ConfigureAwait(false);

		// Assert
		_ = await some.Received().Invoke(value).ConfigureAwait(false);
	}

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg : Msg;
}

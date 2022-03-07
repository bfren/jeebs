// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.MaybeF;

namespace Jeebs_Tests;

public abstract class IfSomeAsync_Tests
{
	public abstract Task Test00_Exception_In_IfSome_Func_Returns_None_With_UnhandledExceptionMsg();

	protected static async Task Test00(Func<Maybe<int>, Func<int, Task>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = Some(F.Rnd.Int);
		var ifSome = Task (int _) => throw new MaybeTestException();

		// Act
		var result = await act(maybe, ifSome).ConfigureAwait(false);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<M.UnhandledExceptionMsg>(none);
	}

	public abstract Task Test01_None_Returns_Original_Option();

	protected static async Task Test01(Func<Maybe<int>, Func<int, Task>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = Create.None<int>();
		var ifSome = Substitute.For<Func<int, Task>>();

		// Act
		var result = await act(maybe, ifSome).ConfigureAwait(false);

		// Assert
		Assert.Same(maybe, result);
		await ifSome.DidNotReceiveWithAnyArgs().Invoke(default).ConfigureAwait(false);
	}

	public abstract Task Test02_Some_Runs_IfSome_Func_And_Returns_Original_Option();

	protected static async Task Test02(Func<Maybe<int>, Func<int, Task>, Task<Maybe<int>>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Some(value);
		var ifSome = Substitute.For<Func<int, Task>>();

		// Act
		var result = await act(maybe, ifSome).ConfigureAwait(false);

		// Assert
		Assert.Same(maybe, result);
		await ifSome.Received().Invoke(value).ConfigureAwait(false);
	}
}

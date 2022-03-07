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

public abstract class AuditAsync_Tests
{
	#region General

	public abstract Task Test00_Null_Args_Returns_Original_Option();

	protected static async Task Test00(Func<Maybe<int>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var result = await act(maybe).ConfigureAwait(false);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract Task Test01_If_Unknown_Maybe_Throws_UnknownOptionException();

	protected static async Task Test01(Func<Maybe<int>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = new FakeMaybe();

		// Act
		var action = Task () => act(maybe);

		// Assert
		_ = await Assert.ThrowsAsync<UnknownMaybeException>(action).ConfigureAwait(false);
	}

	#endregion General

	#region Any

	public abstract Task Test02_Some_Runs_Audit_Action_And_Returns_Original_Option();

	protected static async Task Test02(Func<Maybe<bool>, Action<Maybe<bool>>, Task<Maybe<bool>>> act)
	{
		// Arrange
		var maybe = True;
		var audit = Substitute.For<Action<Maybe<bool>>>();

		// Act
		var result = await act(maybe, audit).ConfigureAwait(false);

		// Assert
		audit.Received().Invoke(maybe);
		Assert.Same(maybe, result);
	}

	public abstract Task Test03_None_Runs_Audit_Action_And_Returns_Original_Option();

	protected static async Task Test03(Func<Maybe<bool>, Action<Maybe<bool>>, Task<Maybe<bool>>> act)
	{
		// Arrange
		var maybe = Create.None<bool>();
		var audit = Substitute.For<Action<Maybe<bool>>>();

		// Act
		var result = await act(maybe, audit).ConfigureAwait(false);

		// Assert
		audit.Received().Invoke(maybe);
		Assert.Same(maybe, result);
	}

	public abstract Task Test04_Some_Runs_Audit_Func_And_Returns_Original_Option();

	protected static async Task Test04(Func<Maybe<bool>, Func<Maybe<bool>, Task>, Task<Maybe<bool>>> act)
	{
		// Arrange
		var maybe = True;
		var audit = Substitute.For<Func<Maybe<bool>, Task>>();

		// Act
		var result = await act(maybe, audit).ConfigureAwait(false);

		// Assert
		await audit.Received().Invoke(maybe).ConfigureAwait(false);
		Assert.Same(maybe, result);
	}

	public abstract Task Test05_None_Runs_Audit_Func_And_Returns_Original_Option();

	protected static async Task Test05(Func<Maybe<bool>, Func<Maybe<bool>, Task>, Task<Maybe<bool>>> act)
	{
		// Arrange
		var maybe = Create.None<bool>();
		var audit = Substitute.For<Func<Maybe<bool>, Task>>();

		// Act
		var result = await act(maybe, audit).ConfigureAwait(false);

		// Assert
		await audit.Received().Invoke(maybe).ConfigureAwait(false);
		Assert.Same(maybe, result);
	}

	public abstract Task Test06_Some_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Option();

	protected static async Task Test06(Func<Maybe<bool>, Action<Maybe<bool>>, Task<Maybe<bool>>> act)
	{
		// Arrange
		var maybe = True;
		var throwException = void (Maybe<bool> _) => throw new MaybeTestException();

		// Act
		var result = await act(maybe, throwException).ConfigureAwait(false);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract Task Test07_None_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Option();

	protected static async Task Test07(Func<Maybe<bool>, Action<Maybe<bool>>, Task<Maybe<bool>>> act)
	{
		// Arrange
		var maybe = Create.None<bool>();
		var throwException = void (Maybe<bool> _) => throw new MaybeTestException();

		// Act
		var result = await act(maybe, throwException).ConfigureAwait(false);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract Task Test08_Some_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Option();

	protected static async Task Test08(Func<Maybe<bool>, Func<Maybe<bool>, Task>, Task<Maybe<bool>>> act)
	{
		// Arrange
		var maybe = True;
		var throwException = Task (Maybe<bool> _) => throw new MaybeTestException();

		// Act
		var result = await act(maybe, throwException).ConfigureAwait(false);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract Task Test09_None_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Option();

	protected static async Task Test09(Func<Maybe<bool>, Func<Maybe<bool>, Task>, Task<Maybe<bool>>> act)
	{
		// Arrange
		var maybe = Create.None<bool>();
		var throwException = Task (Maybe<bool> _) => throw new MaybeTestException();

		// Act
		var result = await act(maybe, throwException).ConfigureAwait(false);

		// Assert
		Assert.Same(maybe, result);
	}

	#endregion Any

	#region Some / None

	public abstract Task Test10_Some_Runs_Some_Action_And_Returns_Original_Option();

	protected static async Task Test10(Func<Maybe<int>, Action<int>, Task<Maybe<int>>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Some(value);
		var some = Substitute.For<Action<int>>();

		// Act
		var result = await act(maybe, some).ConfigureAwait(false);

		// Assert
		some.Received().Invoke(value);
		Assert.Same(maybe, result);
	}

	public abstract Task Test11_Some_Runs_Some_Func_And_Returns_Original_Option();

	protected static async Task Test11(Func<Maybe<int>, Func<int, Task>, Task<Maybe<int>>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var maybe = Some(value);
		var some = Substitute.For<Func<int, Task>>();

		// Act
		var result = await act(maybe, some).ConfigureAwait(false);

		// Assert
		await some.Received().Invoke(value).ConfigureAwait(false);
		Assert.Same(maybe, result);
	}

	public abstract Task Test12_None_Runs_None_Action_And_Returns_Original_Option();

	protected static async Task Test12(Func<Maybe<int>, Action<Msg>, Task<Maybe<int>>> act)
	{
		// Arrange
		var msg = new TestMsg();
		var maybe = None<int>(msg);
		var none = Substitute.For<Action<Msg>>();

		// Act
		var result = await act(maybe, none).ConfigureAwait(false);

		// Assert
		none.Received().Invoke(msg);
		Assert.Same(maybe, result);
	}

	public abstract Task Test13_None_Runs_None_Func_And_Returns_Original_Option();

	protected static async Task Test13(Func<Maybe<int>, Func<Msg, Task>, Task<Maybe<int>>> act)
	{
		// Arrange
		var msg = new TestMsg();
		var maybe = None<int>(msg);
		var none = Substitute.For<Func<Msg, Task>>();

		// Act
		var result = await act(maybe, none).ConfigureAwait(false);

		// Assert
		await none.Received().Invoke(msg).ConfigureAwait(false);
		Assert.Same(maybe, result);
	}

	public abstract Task Test14_Some_Runs_Some_Action_Catches_Exception_And_Returns_Original_Option();

	protected static async Task Test14(Func<Maybe<int>, Action<int>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = Some(F.Rnd.Int);
		var exception = new Exception();
		var throwException = void (int _) => throw exception;

		// Act
		var result = await act(maybe, throwException).ConfigureAwait(false);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract Task Test15_Some_Runs_Some_Func_Catches_Exception_And_Returns_Original_Option();

	protected static async Task Test15(Func<Maybe<int>, Func<int, Task>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = Some(F.Rnd.Int);
		var exception = new Exception();
		var throwException = Task (int _) => throw exception;

		// Act
		var result = await act(maybe, throwException).ConfigureAwait(false);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract Task Test16_None_Runs_None_Action_Catches_Exception_And_Returns_Original_Option();

	protected static async Task Test16(Func<Maybe<int>, Action<Msg>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = Create.None<int>();
		var exception = new Exception();
		var throwException = void (Msg _) => throw exception;

		// Act
		var result = await act(maybe, throwException).ConfigureAwait(false);

		// Assert
		Assert.Same(maybe, result);
	}

	public abstract Task Test17_None_Runs_None_Func_Catches_Exception_And_Returns_Original_Option();

	protected static async Task Test17(Func<Maybe<int>, Func<Msg, Task>, Task<Maybe<int>>> act)
	{
		// Arrange
		var maybe = Create.None<int>();
		var exception = new Exception();
		var throwException = Task (Msg _) => throw exception;

		// Act
		var result = await act(maybe, throwException).ConfigureAwait(false);

		// Assert
		Assert.Same(maybe, result);
	}

	#endregion Some / None

	public record class FakeMaybe : Maybe<int> { }

	public record class TestMsg : Msg;
}

// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;
using static F.OptionF.Msg;

namespace Jeebs_Tests;

public abstract class BindAsync_Tests
{
	public abstract Task Test00_If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg();

	protected static async Task Test00(Func<Option<int>, Func<int, Task<Option<string>>>, Task<Option<string>>> act)
	{
		// Arrange
		var option = new FakeOption();
		var bind = Substitute.For<Func<int, Task<Option<string>>>>();

		// Act
		var result = await act(option, bind);

		// Assert
		var none = result.AssertNone();
		var msg = Assert.IsType<UnhandledExceptionMsg>(none);
		Assert.IsType<UnknownOptionException>(msg.Exception);
	}

	public abstract Task Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg();

	protected static async Task Test01(Func<Option<int>, Func<int, Task<Option<string>>>, Task<Option<string>>> act)
	{
		// Arrange
		var option = Some(F.Rnd.Int);
		var exception = new Exception();
		Task<Option<string>> throwFunc() => throw exception;

		// Act
		var result = await act(option, _ => throwFunc());

		// Assert
		var none = result.AssertNone();
		Assert.IsType<UnhandledExceptionMsg>(none);
	}

	public abstract Task Test02_If_None_Gets_None();

	protected static async Task Test02(Func<Option<int>, Func<int, Task<Option<string>>>, Task<Option<string>>> act)
	{
		// Arrange
		var option = Create.None<int>();
		var bind = Substitute.For<Func<int, Task<Option<string>>>>();

		// Act
		var result = await act(option, bind);

		// Assert
		result.AssertNone();
	}

	public abstract Task Test03_If_None_With_Reason_Gets_None_With_Same_Reason();

	protected static async Task Test03(Func<Option<int>, Func<int, Task<Option<string>>>, Task<Option<string>>> act)
	{
		// Arrange
		var msg = new TestMsg();
		var option = None<int>(msg);
		var bind = Substitute.For<Func<int, Task<Option<string>>>>();

		// Act
		var result = await act(option, bind);

		// Assert
		var none = result.AssertNone();
		Assert.Same(msg, none);
	}

	public abstract Task Test04_If_Some_Runs_Bind_Function();

	protected static async Task Test04(Func<Option<int>, Func<int, Task<Option<string>>>, Task<Option<string>>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var option = Some(value);
		var bind = Substitute.For<Func<int, Task<Option<string>>>>();

		// Act
		await act(option, bind);

		// Assert
		await bind.Received().Invoke(value);
	}

	public record class FakeOption : Option<int> { }

	public record class TestMsg : IMsg { }
}

// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs_Tests;

public abstract class IfSome_Tests
{
	public abstract void Test00_Exception_In_IfSome_Action_Returns_None_With_UnhandledExceptionMsg();

	protected static void Test00(Func<Option<int>, Action<int>, Option<int>> act)
	{
		// Arrange
		var option = Some(F.Rnd.Int);
		var ifSome = void (int _) => throw new Exception();

		// Act
		var result = act(option, ifSome);

		// Assert
		var none = result.AssertNone();
		Assert.IsType<M.UnhandledExceptionMsg>(none);
	}

	public abstract void Test01_None_Returns_Original_Option();

	protected static void Test01(Func<Option<int>, Action<int>, Option<int>> act)
	{
		// Arrange
		var option = Create.None<int>();
		var ifSome = Substitute.For<Action<int>>();

		// Act
		var result = act(option, ifSome);

		// Assert
		Assert.Same(option, result);
		ifSome.DidNotReceiveWithAnyArgs().Invoke(default);
	}

	public abstract void Test02_Some_Runs_IfSome_Action_And_Returns_Original_Option();

	protected static void Test02(Func<Option<int>, Action<int>, Option<int>> act)
	{
		// Arrange
		var value = F.Rnd.Int;
		var option = Some(value);
		var ifSome = Substitute.For<Action<int>>();

		// Act
		var result = act(option, ifSome);

		// Assert
		Assert.Same(option, result);
		ifSome.Received().Invoke(value);
	}
}

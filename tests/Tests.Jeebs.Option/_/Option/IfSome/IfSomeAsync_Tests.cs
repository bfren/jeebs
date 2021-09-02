// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Option_Tests;

public class IfSomeAsync_Tests : Jeebs_Tests.IfSomeAsync_Tests
{
	[Fact]
	public override async Task Test00_Exception_In_IfSome_Func_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test00((opt, ifSome) => opt.IfSomeAsync(ifSome));
	}

	[Fact]
	public override async Task Test01_None_Returns_Original_Option()
	{
		await Test01((opt, ifSome) => opt.IfSomeAsync(ifSome));
	}

	[Fact]
	public override async Task Test02_Some_Runs_IfSome_Func_And_Returns_Original_Option()
	{
		await Test02((opt, ifSome) => opt.IfSomeAsync(ifSome));
	}
}

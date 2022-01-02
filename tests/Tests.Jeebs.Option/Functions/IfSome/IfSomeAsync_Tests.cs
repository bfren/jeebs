// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests;

public class IfSomeAsync_Tests : Jeebs_Tests.IfSomeAsync_Tests
{
	[Fact]
	public override async Task Test00_Exception_In_IfSome_Func_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test00((opt, ifSome) => IfSomeAsync(opt, ifSome)).ConfigureAwait(false);
		await Test00((opt, ifSome) => IfSomeAsync(opt.AsTask, ifSome)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_None_Returns_Original_Option()
	{
		await Test01((opt, ifSome) => IfSomeAsync(opt, ifSome)).ConfigureAwait(false);
		await Test01((opt, ifSome) => IfSomeAsync(opt.AsTask, ifSome)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_Some_Runs_IfSome_Func_And_Returns_Original_Option()
	{
		await Test02((opt, ifSome) => IfSomeAsync(opt, ifSome)).ConfigureAwait(false);
		await Test02((opt, ifSome) => IfSomeAsync(opt.AsTask, ifSome)).ConfigureAwait(false);
	}
}

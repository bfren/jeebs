// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Xunit;

namespace Jeebs.Option_Tests;

public class IfNullAsync_Tests : Jeebs_Tests.IfNullAsync_Tests
{
	[Fact]
	public override async Task Test00_Exception_In_NullValue_Func_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test00((opt, ifNull) => opt.IfNullAsync(ifNull)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_Some_With_Null_Value_Runs_IfNull_Func()
	{
		await Test01((opt, ifNull) => opt.IfNullAsync(ifNull)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_None_With_NullValueMsg_Runs_IfNull_Func()
	{
		await Test02((opt, ifNull) => opt.IfNullAsync(ifNull)).ConfigureAwait(false);
	}

	#region Unused

	[Fact]
	public override Task Test03_Some_With_Null_Value_Runs_IfNull_Func_Returns_None_With_Reason() =>
		Task.CompletedTask;

	[Fact]
	public override Task Test04_None_With_NullValueMsg_Runs_IfNull_Func_Returns_None_With_Reason() =>
		Task.CompletedTask;

	#endregion
}

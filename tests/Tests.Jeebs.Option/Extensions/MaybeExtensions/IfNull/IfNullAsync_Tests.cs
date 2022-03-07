// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Xunit;

namespace Jeebs.MaybeExtensions_Tests;

public class IfNullAsync_Tests : Jeebs_Tests.IfNullAsync_Tests
{
	[Fact]
	public override async Task Test00_Exception_In_NullValue_Func_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test00((mbe, ifNull) => mbe.AsTask.IfNullAsync(ifNull)).ConfigureAwait(false);
		await Test00((mbe, ifNull) => mbe.AsTask.IfNullAsync(() => ifNull().GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test00((mbe, ifNull) => mbe.AsTask.IfNullAsync(() => { ifNull(); return new TestMsg(); })).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_Some_With_Null_Value_Runs_IfNull_Func()
	{
		await Test01((mbe, ifNull) => mbe.AsTask.IfNullAsync(ifNull)).ConfigureAwait(false);
		await Test01((mbe, ifNull) => mbe.AsTask.IfNullAsync(() => ifNull().GetAwaiter().GetResult())).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_None_With_NullValueMsg_Runs_IfNull_Func()
	{
		await Test02((mbe, ifNull) => mbe.AsTask.IfNullAsync(ifNull)).ConfigureAwait(false);
		await Test02((mbe, ifNull) => mbe.AsTask.IfNullAsync(() => ifNull().GetAwaiter().GetResult())).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test03_Some_With_Null_Value_Runs_IfNull_Func_Returns_None_With_Reason()
	{
		await Test03((mbe, ifNull) => mbe.AsTask.IfNullAsync(ifNull)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test04_None_With_NullValueMsg_Runs_IfNull_Func_Returns_None_With_Reason()
	{
		await Test04((mbe, ifNull) => mbe.AsTask.IfNullAsync(ifNull)).ConfigureAwait(false);
	}
}

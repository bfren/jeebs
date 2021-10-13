// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Xunit;

namespace Jeebs.OptionExtensions_Tests;

public class MapAsync_Tests : Jeebs_Tests.MapAsync_Tests
{
	[Fact]
	public override async Task Test00_If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test00((opt, map, handler) => opt.AsTask.MapAsync(x => map(x).GetAwaiter().GetResult(), handler));
		await Test00((opt, map, handler) => opt.AsTask.MapAsync(map, handler));
	}

	[Fact]
	public override async Task Test01_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test01((opt, map, handler) => opt.AsTask.MapAsync(x => map(x).GetAwaiter().GetResult(), handler));
		await Test01((opt, map, handler) => opt.AsTask.MapAsync(map, handler));
	}

	[Fact]
	public override async Task Test02_Exception_Thrown_With_Handler_Calls_Handler_Returns_None()
	{
		await Test02((opt, map, handler) => opt.AsTask.MapAsync(x => map(x).GetAwaiter().GetResult(), handler));
		await Test02((opt, map, handler) => opt.AsTask.MapAsync(map, handler));
	}

	[Fact]
	public override async Task Test03_If_None_Returns_None()
	{
		await Test03((opt, map, handler) => opt.AsTask.MapAsync(x => map(x).GetAwaiter().GetResult(), handler));
		await Test03((opt, map, handler) => opt.AsTask.MapAsync(map, handler));
	}

	[Fact]
	public override async Task Test04_If_None_With_Reason_Returns_None_With_Same_Reason()
	{
		await Test04((opt, map, handler) => opt.AsTask.MapAsync(x => map(x).GetAwaiter().GetResult(), handler));
		await Test04((opt, map, handler) => opt.AsTask.MapAsync(map, handler));
	}

	[Fact]
	public override async Task Test05_If_Some_Runs_Map_Function()
	{
		await Test05((opt, map, handler) => opt.AsTask.MapAsync(x => map(x).GetAwaiter().GetResult(), handler));
		await Test05((opt, map, handler) => opt.AsTask.MapAsync(map, handler));
	}
}

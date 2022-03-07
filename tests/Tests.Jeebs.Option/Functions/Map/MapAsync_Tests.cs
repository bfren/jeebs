// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System.Threading.Tasks;
using Xunit;
using static F.MaybeF;

namespace F.MaybeF_Tests;

public class MapAsync_Tests : Jeebs_Tests.MapAsync_Tests
{
	[Fact]
	public override async Task Test00_If_Unknown_Maybe_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test00((mbe, map, handler) => MapAsync(mbe, map, handler)).ConfigureAwait(false);
		await Test00((mbe, map, handler) => MapAsync(mbe.AsTask, map, handler)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_Exception_Thrown_Without_Handler_Returns_None_With_UnhandledExceptionMsg()
	{
		await Test01((mbe, map, handler) => MapAsync(mbe, map, handler)).ConfigureAwait(false);
		await Test01((mbe, map, handler) => MapAsync(mbe.AsTask, map, handler)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_Exception_Thrown_With_Handler_Calls_Handler_Returns_None()
	{
		await Test02((mbe, map, handler) => MapAsync(mbe, map, handler)).ConfigureAwait(false);
		await Test02((mbe, map, handler) => MapAsync(mbe.AsTask, map, handler)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test03_If_None_Returns_None()
	{
		await Test03((mbe, map, handler) => MapAsync(mbe, map, handler)).ConfigureAwait(false);
		await Test03((mbe, map, handler) => MapAsync(mbe.AsTask, map, handler)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test04_If_None_With_Reason_Returns_None_With_Same_Reason()
	{
		await Test04((mbe, map, handler) => MapAsync(mbe, map, handler)).ConfigureAwait(false);
		await Test04((mbe, map, handler) => MapAsync(mbe.AsTask, map, handler)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test05_If_Some_Runs_Map_Function()
	{
		await Test05((mbe, map, handler) => MapAsync(mbe, map, handler)).ConfigureAwait(false);
		await Test05((mbe, map, handler) => MapAsync(mbe.AsTask, map, handler)).ConfigureAwait(false);
	}
}

// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.MaybeF;

namespace F.MaybeF_Tests;

public class SwitchAsync_Tests : Jeebs_Tests.SwitchAsync_Tests
{
	[Fact]
	public override async Task Test00_If_Unknown_Maybe_Throws_UnknownOptionException()
	{
		var some = Substitute.For<Func<int, Task<string>>>();
		var none = Substitute.For<Func<Msg, Task<string>>>();
		await Test00(mbe => SwitchAsync(mbe, some, none)).ConfigureAwait(false);
		await Test00(mbe => SwitchAsync(mbe.AsTask, some, none)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_If_None_Runs_None_Func_With_Reason()
	{
		var some = Substitute.For<Func<int, Task<string>>>();
		await Test01((mbe, none) => SwitchAsync(mbe, some, none)).ConfigureAwait(false);
		await Test01((mbe, none) => SwitchAsync(mbe.AsTask, some, none)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_If_Some_Runs_Some_Func_With_Value()
	{
		var none = Substitute.For<Func<Msg, Task<string>>>();
		await Test02((mbe, some) => SwitchAsync(mbe, some, none)).ConfigureAwait(false);
		await Test02((mbe, some) => SwitchAsync(mbe.AsTask, some, none)).ConfigureAwait(false);
	}
}

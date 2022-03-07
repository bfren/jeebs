﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.MaybeF;

namespace F.MaybeF_Tests;

public class UnwrapAsync_Tests : Jeebs_Tests.UnwrapAsync_Tests
{
	[Fact]
	public override async Task Test00_None_Runs_IfNone_Func_Returns_Value()
	{
		await Test00((mbe, ifNone) => UnwrapAsync(mbe, x => x.Value(ifNone()))).ConfigureAwait(false);
		await Test00((mbe, ifNone) => UnwrapAsync(mbe, x => x.Value(ifNone))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_None_With_Reason_Runs_IfNone_Func_Passes_Reason_Returns_Value()
	{
		await Test01((mbe, ifNone) => UnwrapAsync(mbe, x => x.Value(ifNone))).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_Some_Returns_Value()
	{
		await Test02(mbe => UnwrapAsync(mbe, x => x.Value(Rnd.Int))).ConfigureAwait(false);
		await Test02(mbe => UnwrapAsync(mbe, x => x.Value(Substitute.For<Func<int>>()))).ConfigureAwait(false);
		await Test02(mbe => UnwrapAsync(mbe, x => x.Value(Substitute.For<Func<Msg, int>>()))).ConfigureAwait(false);
	}
}

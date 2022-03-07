﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.MaybeExtensions_Tests;

public class SwitchAsync_Tests : Jeebs_Tests.SwitchAsync_Tests
{
	[Fact]
	public override async Task Test00_If_Unknown_Maybe_Throws_UnknownOptionException()
	{
		await Test00(mbe => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), F.Rnd.Str)).ConfigureAwait(false);
		await Test00(mbe => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), F.Rnd.Str)).ConfigureAwait(false);
		await Test00(mbe => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), Task.FromResult(F.Rnd.Str))).ConfigureAwait(false);
		await Test00(mbe => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Task.FromResult(F.Rnd.Str))).ConfigureAwait(false);
		await Test00(mbe => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), Substitute.For<Func<string>>())).ConfigureAwait(false);
		await Test00(mbe => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<string>>())).ConfigureAwait(false);
		await Test00(mbe => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), Substitute.For<Func<Task<string>>>())).ConfigureAwait(false);
		await Test00(mbe => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<Task<string>>>())).ConfigureAwait(false);
		await Test00(mbe => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), Substitute.For<Func<Msg, string>>())).ConfigureAwait(false);
		await Test00(mbe => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<Msg, string>>())).ConfigureAwait(false);
		await Test00(mbe => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), Substitute.For<Func<Msg, Task<string>>>())).ConfigureAwait(false);
		await Test00(mbe => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<Msg, Task<string>>>())).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_If_None_Runs_None_Func_With_Reason()
	{
		await Test01((mbe, none) => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), none(new TestMsg()).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test01((mbe, none) => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), none(new TestMsg()).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test01((mbe, none) => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), none(new TestMsg()))).ConfigureAwait(false);
		await Test01((mbe, none) => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), none(new TestMsg()))).ConfigureAwait(false);
		await Test01((mbe, none) => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), () => none(new TestMsg()).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test01((mbe, none) => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), () => none(new TestMsg()).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test01((mbe, none) => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), () => none(new TestMsg()))).ConfigureAwait(false);
		await Test01((mbe, none) => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), () => none(new TestMsg()))).ConfigureAwait(false);
		await Test01((mbe, none) => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), x => none(x).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test01((mbe, none) => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), x => none(x).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test01((mbe, none) => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), none)).ConfigureAwait(false);
		await Test01((mbe, none) => mbe.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), none)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_If_Some_Runs_Some_Func_With_Value()
	{
		await Test02((mbe, some) => mbe.AsTask.SwitchAsync(x => some(x).GetAwaiter().GetResult(), F.Rnd.Str)).ConfigureAwait(false);
		await Test02((mbe, some) => mbe.AsTask.SwitchAsync(some, F.Rnd.Str)).ConfigureAwait(false);
		await Test02((mbe, some) => mbe.AsTask.SwitchAsync(x => some(x).GetAwaiter().GetResult(), Task.FromResult(F.Rnd.Str))).ConfigureAwait(false);
		await Test02((mbe, some) => mbe.AsTask.SwitchAsync(some, Task.FromResult(F.Rnd.Str))).ConfigureAwait(false);
		await Test02((mbe, some) => mbe.AsTask.SwitchAsync(x => some(x).GetAwaiter().GetResult(), Substitute.For<Func<string>>())).ConfigureAwait(false);
		await Test02((mbe, some) => mbe.AsTask.SwitchAsync(some, Substitute.For<Func<string>>())).ConfigureAwait(false);
		await Test02((mbe, some) => mbe.AsTask.SwitchAsync(x => some(x).GetAwaiter().GetResult(), Substitute.For<Func<Task<string>>>())).ConfigureAwait(false);
		await Test02((mbe, some) => mbe.AsTask.SwitchAsync(some, Substitute.For<Func<Task<string>>>())).ConfigureAwait(false);
		await Test02((mbe, some) => mbe.AsTask.SwitchAsync(x => some(x).GetAwaiter().GetResult(), Substitute.For<Func<Msg, string>>())).ConfigureAwait(false);
		await Test02((mbe, some) => mbe.AsTask.SwitchAsync(some, Substitute.For<Func<Msg, string>>())).ConfigureAwait(false);
		await Test02((mbe, some) => mbe.AsTask.SwitchAsync(x => some(x).GetAwaiter().GetResult(), Substitute.For<Func<Msg, Task<string>>>())).ConfigureAwait(false);
		await Test02((mbe, some) => mbe.AsTask.SwitchAsync(some, Substitute.For<Func<Msg, Task<string>>>())).ConfigureAwait(false);
	}
}
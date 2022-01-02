// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.OptionExtensions_Tests;

public class SwitchAsync_Tests : Jeebs_Tests.SwitchAsync_Tests
{
	[Fact]
	public override async Task Test00_If_Unknown_Option_Throws_UnknownOptionException()
	{
		await Test00(opt => opt.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), F.Rnd.Str)).ConfigureAwait(false);
		await Test00(opt => opt.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), F.Rnd.Str)).ConfigureAwait(false);
		await Test00(opt => opt.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), Task.FromResult(F.Rnd.Str))).ConfigureAwait(false);
		await Test00(opt => opt.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Task.FromResult(F.Rnd.Str))).ConfigureAwait(false);
		await Test00(opt => opt.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), Substitute.For<Func<string>>())).ConfigureAwait(false);
		await Test00(opt => opt.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<string>>())).ConfigureAwait(false);
		await Test00(opt => opt.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), Substitute.For<Func<Task<string>>>())).ConfigureAwait(false);
		await Test00(opt => opt.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<Task<string>>>())).ConfigureAwait(false);
		await Test00(opt => opt.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), Substitute.For<Func<Msg, string>>())).ConfigureAwait(false);
		await Test00(opt => opt.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<Msg, string>>())).ConfigureAwait(false);
		await Test00(opt => opt.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), Substitute.For<Func<Msg, Task<string>>>())).ConfigureAwait(false);
		await Test00(opt => opt.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), Substitute.For<Func<Msg, Task<string>>>())).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test01_If_None_Runs_None_Func_With_Reason()
	{
		var some = Substitute.For<Func<int, Task<string>>>();
		await Test01((opt, none) => opt.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), none(new TestMsg()).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test01((opt, none) => opt.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), none(new TestMsg()).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test01((opt, none) => opt.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), none(new TestMsg()))).ConfigureAwait(false);
		await Test01((opt, none) => opt.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), none(new TestMsg()))).ConfigureAwait(false);
		await Test01((opt, none) => opt.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), () => none(new TestMsg()).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test01((opt, none) => opt.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), () => none(new TestMsg()).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test01((opt, none) => opt.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), () => none(new TestMsg()))).ConfigureAwait(false);
		await Test01((opt, none) => opt.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), () => none(new TestMsg()))).ConfigureAwait(false);
		await Test01((opt, none) => opt.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), x => none(x).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test01((opt, none) => opt.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), x => none(x).GetAwaiter().GetResult())).ConfigureAwait(false);
		await Test01((opt, none) => opt.AsTask.SwitchAsync(Substitute.For<Func<int, string>>(), none)).ConfigureAwait(false);
		await Test01((opt, none) => opt.AsTask.SwitchAsync(Substitute.For<Func<int, Task<string>>>(), none)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test02_If_Some_Runs_Some_Func_With_Value()
	{
		var none = Substitute.For<Func<Msg, Task<string>>>();
		await Test02((opt, some) => opt.AsTask.SwitchAsync(x => some(x).GetAwaiter().GetResult(), F.Rnd.Str)).ConfigureAwait(false);
		await Test02((opt, some) => opt.AsTask.SwitchAsync(some, F.Rnd.Str)).ConfigureAwait(false);
		await Test02((opt, some) => opt.AsTask.SwitchAsync(x => some(x).GetAwaiter().GetResult(), Task.FromResult(F.Rnd.Str))).ConfigureAwait(false);
		await Test02((opt, some) => opt.AsTask.SwitchAsync(some, Task.FromResult(F.Rnd.Str))).ConfigureAwait(false);
		await Test02((opt, some) => opt.AsTask.SwitchAsync(x => some(x).GetAwaiter().GetResult(), Substitute.For<Func<string>>())).ConfigureAwait(false);
		await Test02((opt, some) => opt.AsTask.SwitchAsync(some, Substitute.For<Func<string>>())).ConfigureAwait(false);
		await Test02((opt, some) => opt.AsTask.SwitchAsync(x => some(x).GetAwaiter().GetResult(), Substitute.For<Func<Task<string>>>())).ConfigureAwait(false);
		await Test02((opt, some) => opt.AsTask.SwitchAsync(some, Substitute.For<Func<Task<string>>>())).ConfigureAwait(false);
		await Test02((opt, some) => opt.AsTask.SwitchAsync(x => some(x).GetAwaiter().GetResult(), Substitute.For<Func<Msg, string>>())).ConfigureAwait(false);
		await Test02((opt, some) => opt.AsTask.SwitchAsync(some, Substitute.For<Func<Msg, string>>())).ConfigureAwait(false);
		await Test02((opt, some) => opt.AsTask.SwitchAsync(x => some(x).GetAwaiter().GetResult(), Substitute.For<Func<Msg, Task<string>>>())).ConfigureAwait(false);
		await Test02((opt, some) => opt.AsTask.SwitchAsync(some, Substitute.For<Func<Msg, Task<string>>>())).ConfigureAwait(false);
	}
}

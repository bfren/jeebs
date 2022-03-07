// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.MaybeExtensions_Tests;

public class AuditAsync_Tests : Jeebs_Tests.AuditAsync_Tests
{
	#region General

	[Fact]
	public override async Task Test01_If_Unknown_Maybe_Throws_UnknownOptionException()
	{
		var anyA = Substitute.For<Action<Maybe<int>>>();
		var anyF = Substitute.For<Func<Maybe<int>, Task>>();
		var someA = Substitute.For<Action<int>>();
		var someF = Substitute.For<Func<int, Task>>();
		var noneA = Substitute.For<Action<Msg>>();
		var noneF = Substitute.For<Func<Msg, Task>>();

		await Test01(mbe => mbe.AsTask.AuditAsync(anyA)).ConfigureAwait(false);
		await Test01(mbe => mbe.AsTask.AuditAsync(anyF)).ConfigureAwait(false);
		await Test01(mbe => mbe.AsTask.AuditAsync(someA)).ConfigureAwait(false);
		await Test01(mbe => mbe.AsTask.AuditAsync(someF)).ConfigureAwait(false);
		await Test01(mbe => mbe.AsTask.AuditAsync(noneA)).ConfigureAwait(false);
		await Test01(mbe => mbe.AsTask.AuditAsync(noneF)).ConfigureAwait(false);
		await Test01(mbe => mbe.AsTask.AuditAsync(someA, noneA)).ConfigureAwait(false);
		await Test01(mbe => mbe.AsTask.AuditAsync(someF, noneF)).ConfigureAwait(false);
	}

	#endregion

	#region Any

	[Fact]
	public override async Task Test02_Some_Runs_Audit_Action_And_Returns_Original_Option()
	{
		await Test02((some, any) => some.AsTask.AuditAsync(any)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test03_None_Runs_Audit_Action_And_Returns_Original_Option()
	{
		await Test03((none, any) => none.AsTask.AuditAsync(any)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test04_Some_Runs_Audit_Func_And_Returns_Original_Option()
	{
		await Test04((some, any) => some.AsTask.AuditAsync(any)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test05_None_Runs_Audit_Func_And_Returns_Original_Option()
	{
		await Test05((none, any) => none.AsTask.AuditAsync(any)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test06_Some_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Option()
	{
		await Test06((some, any) => some.AsTask.AuditAsync(any)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test07_None_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Option()
	{
		await Test07((none, any) => none.AsTask.AuditAsync(any)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test08_Some_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Option()
	{
		await Test08((some, any) => some.AsTask.AuditAsync(any)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test09_None_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Option()
	{
		await Test09((none, any) => none.AsTask.AuditAsync(any)).ConfigureAwait(false);
	}

	#endregion

	#region Some / None
	[Fact]
	public override async Task Test10_Some_Runs_Some_Action_And_Returns_Original_Option()
	{
		var none = Substitute.For<Action<Msg>>();

		await Test10((mbe, some) => mbe.AsTask.AuditAsync(some)).ConfigureAwait(false);
		await Test10((mbe, some) => mbe.AsTask.AuditAsync(some, none)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test11_Some_Runs_Some_Func_And_Returns_Original_Option()
	{
		var none = Substitute.For<Func<Msg, Task>>();

		await Test11((mbe, some) => mbe.AsTask.AuditAsync(some)).ConfigureAwait(false);
		await Test11((mbe, some) => mbe.AsTask.AuditAsync(some, none)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test12_None_Runs_None_Action_And_Returns_Original_Option()
	{
		var some = Substitute.For<Action<int>>();

		await Test12((mbe, none) => mbe.AsTask.AuditAsync(none)).ConfigureAwait(false);
		await Test12((mbe, none) => mbe.AsTask.AuditAsync(some, none)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test13_None_Runs_None_Func_And_Returns_Original_Option()
	{
		var some = Substitute.For<Func<int, Task>>();

		await Test13((mbe, none) => mbe.AsTask.AuditAsync(none)).ConfigureAwait(false);
		await Test13((mbe, none) => mbe.AsTask.AuditAsync(some, none)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test14_Some_Runs_Some_Action_Catches_Exception_And_Returns_Original_Option()
	{
		var none = Substitute.For<Action<Msg>>();

		await Test14((mbe, some) => mbe.AsTask.AuditAsync(some)).ConfigureAwait(false);
		await Test14((mbe, some) => mbe.AsTask.AuditAsync(some, none)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test15_Some_Runs_Some_Func_Catches_Exception_And_Returns_Original_Option()
	{
		var none = Substitute.For<Func<Msg, Task>>();

		await Test15((mbe, some) => mbe.AsTask.AuditAsync(some)).ConfigureAwait(false);
		await Test15((mbe, some) => mbe.AsTask.AuditAsync(some, none)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test16_None_Runs_None_Action_Catches_Exception_And_Returns_Original_Option()
	{
		var some = Substitute.For<Action<int>>();

		await Test16((mbe, none) => mbe.AsTask.AuditAsync(none)).ConfigureAwait(false);
		await Test16((mbe, none) => mbe.AsTask.AuditAsync(some, none)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test17_None_Runs_None_Func_Catches_Exception_And_Returns_Original_Option()
	{
		var some = Substitute.For<Func<int, Task>>();

		await Test17((mbe, none) => mbe.AsTask.AuditAsync(none)).ConfigureAwait(false);
		await Test17((mbe, none) => mbe.AsTask.AuditAsync(some, none)).ConfigureAwait(false);
	}

	#endregion

	#region Unused

	public override Task Test00_Null_Args_Returns_Original_Option() =>
		Task.CompletedTask;

	#endregion
}

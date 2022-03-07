// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Maybe_Tests;

public class AuditAsync_Tests : Jeebs_Tests.AuditAsync_Tests
{
	#region General

	[Fact]
	public override async Task Test01_If_Unknown_Maybe_Throws_UnknownOptionException()
	{
		var any = Substitute.For<Func<Maybe<int>, Task>>();
		var some = Substitute.For<Func<int, Task>>();
		var none = Substitute.For<Func<Msg, Task>>();

		await Test01(mbe => mbe.AuditAsync(any)).ConfigureAwait(false);
		await Test01(mbe => mbe.AuditAsync(some)).ConfigureAwait(false);
		await Test01(mbe => mbe.AuditAsync(none)).ConfigureAwait(false);
		await Test01(mbe => mbe.AuditAsync(some, none)).ConfigureAwait(false);
	}

	#endregion General

	#region Any

	[Fact]
	public override async Task Test04_Some_Runs_Audit_Func_And_Returns_Original_Option()
	{
		await Test04((mbe, any) => mbe.AuditAsync(any)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test05_None_Runs_Audit_Func_And_Returns_Original_Option()
	{
		await Test05((mbe, any) => mbe.AuditAsync(any)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test08_Some_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Option()
	{
		await Test08((mbe, any) => mbe.AuditAsync(any)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test09_None_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Option()
	{
		await Test09((mbe, any) => mbe.AuditAsync(any)).ConfigureAwait(false);
	}

	#endregion Any

	#region Some / None

	[Fact]
	public override async Task Test11_Some_Runs_Some_Func_And_Returns_Original_Option()
	{
		var none = Substitute.For<Func<Msg, Task>>();

		await Test11((mbe, some) => mbe.AuditAsync(some)).ConfigureAwait(false);
		await Test11((mbe, some) => mbe.AuditAsync(some, none)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test13_None_Runs_None_Func_And_Returns_Original_Option()
	{
		var some = Substitute.For<Func<int, Task>>();

		await Test13((mbe, none) => mbe.AuditAsync(none)).ConfigureAwait(false);
		await Test13((mbe, none) => mbe.AuditAsync(some, none)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test15_Some_Runs_Some_Func_Catches_Exception_And_Returns_Original_Option()
	{
		var none = Substitute.For<Func<Msg, Task>>();

		await Test15((mbe, some) => mbe.AuditAsync(some)).ConfigureAwait(false);
		await Test15((mbe, some) => mbe.AuditAsync(some, none)).ConfigureAwait(false);
	}

	[Fact]
	public override async Task Test17_None_Runs_None_Func_Catches_Exception_And_Returns_Original_Option()
	{
		var some = Substitute.For<Func<int, Task>>();

		await Test17((mbe, none) => mbe.AuditAsync(none)).ConfigureAwait(false);
		await Test17((mbe, none) => mbe.AuditAsync(some, none)).ConfigureAwait(false);
	}

	#endregion Some / None

	#region Unused

	public override Task Test00_Null_Args_Returns_Original_Option() =>
		Task.CompletedTask;

	public override Task Test02_Some_Runs_Audit_Action_And_Returns_Original_Option() =>
		Task.CompletedTask;

	public override Task Test03_None_Runs_Audit_Action_And_Returns_Original_Option() =>
		Task.CompletedTask;

	public override Task Test06_Some_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Option() =>
		Task.CompletedTask;

	public override Task Test07_None_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Option() =>
		Task.CompletedTask;

	public override Task Test10_Some_Runs_Some_Action_And_Returns_Original_Option() =>
		Task.CompletedTask;

	public override Task Test12_None_Runs_None_Action_And_Returns_Original_Option() =>
		Task.CompletedTask;

	public override Task Test14_Some_Runs_Some_Action_Catches_Exception_And_Returns_Original_Option() =>
		Task.CompletedTask;

	public override Task Test16_None_Runs_None_Action_Catches_Exception_And_Returns_Original_Option() =>
		Task.CompletedTask;

	#endregion Unused
}

// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.OptionExtensions_Tests
{
	public class AuditAsync_Tests : Jeebs_Tests.AuditAsync_Tests
	{
		#region General

		[Fact]
		public override async Task Test01_If_Unknown_Option_Throws_UnknownOptionException()
		{
			var anyA = Substitute.For<Action<Option<int>>>();
			var anyF = Substitute.For<Func<Option<int>, Task>>();
			var someA = Substitute.For<Action<int>>();
			var someF = Substitute.For<Func<int, Task>>();
			var noneA = Substitute.For<Action<IMsg>>();
			var noneF = Substitute.For<Func<IMsg, Task>>();

			await Test01(opt => opt.AsTask.AuditAsync(anyA));
			await Test01(opt => opt.AsTask.AuditAsync(anyF));
			await Test01(opt => opt.AsTask.AuditSwitchAsync(someA));
			await Test01(opt => opt.AsTask.AuditSwitchAsync(someF));
			await Test01(opt => opt.AsTask.AuditSwitchAsync(noneA));
			await Test01(opt => opt.AsTask.AuditSwitchAsync(noneF));
			await Test01(opt => opt.AsTask.AuditSwitchAsync(someA, noneA));
			await Test01(opt => opt.AsTask.AuditSwitchAsync(someF, noneF));
		}

		#endregion

		#region Any

		[Fact]
		public override async Task Test02_Some_Runs_Audit_Action_And_Returns_Original_Option()
		{
			await Test02((some, any) => some.AsTask.AuditAsync(any));
		}

		[Fact]
		public override async Task Test03_None_Runs_Audit_Action_And_Returns_Original_Option()
		{
			await Test03((none, any) => none.AsTask.AuditAsync(any));
		}

		[Fact]
		public override async Task Test04_Some_Runs_Audit_Func_And_Returns_Original_Option()
		{
			await Test04((some, any) => some.AsTask.AuditAsync(any));
		}

		[Fact]
		public override async Task Test05_None_Runs_Audit_Func_And_Returns_Original_Option()
		{
			await Test05((none, any) => none.AsTask.AuditAsync(any));
		}

		[Fact]
		public override async Task Test06_Some_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Option()
		{
			await Test06((some, any) => some.AsTask.AuditAsync(any));
		}

		[Fact]
		public override async Task Test07_None_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Option()
		{
			await Test07((none, any) => none.AsTask.AuditAsync(any));
		}

		[Fact]
		public override async Task Test08_Some_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Option()
		{
			await Test08((some, any) => some.AsTask.AuditAsync(any));
		}

		[Fact]
		public override async Task Test09_None_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Option()
		{
			await Test09((none, any) => none.AsTask.AuditAsync(any));
		}

		#endregion

		#region Some / None
		[Fact]
		public override async Task Test10_Some_Runs_Some_Action_And_Returns_Original_Option()
		{
			var none = Substitute.For<Action<IMsg>>();

			await Test10((opt, some) => opt.AsTask.AuditSwitchAsync(some));
			await Test10((opt, some) => opt.AsTask.AuditSwitchAsync(some, none));
		}

		[Fact]
		public override async Task Test11_Some_Runs_Some_Func_And_Returns_Original_Option()
		{
			var none = Substitute.For<Func<IMsg, Task>>();

			await Test11((opt, some) => opt.AsTask.AuditSwitchAsync(some));
			await Test11((opt, some) => opt.AsTask.AuditSwitchAsync(some, none));
		}

		[Fact]
		public override async Task Test12_None_Runs_None_Action_And_Returns_Original_Option()
		{
			var some = Substitute.For<Action<int>>();

			await Test12((opt, none) => opt.AsTask.AuditSwitchAsync(none));
			await Test12((opt, none) => opt.AsTask.AuditSwitchAsync(some, none));
		}

		[Fact]
		public override async Task Test13_None_Runs_None_Func_And_Returns_Original_Option()
		{
			var some = Substitute.For<Func<int, Task>>();

			await Test13((opt, none) => opt.AsTask.AuditSwitchAsync(none));
			await Test13((opt, none) => opt.AsTask.AuditSwitchAsync(some, none));
		}

		[Fact]
		public override async Task Test14_Some_Runs_Some_Action_Catches_Exception_And_Returns_Original_Option()
		{
			var none = Substitute.For<Action<IMsg>>();

			await Test14((opt, some) => opt.AsTask.AuditSwitchAsync(some));
			await Test14((opt, some) => opt.AsTask.AuditSwitchAsync(some, none));
		}

		[Fact]
		public override async Task Test15_Some_Runs_Some_Func_Catches_Exception_And_Returns_Original_Option()
		{
			var none = Substitute.For<Func<IMsg, Task>>();

			await Test15((opt, some) => opt.AsTask.AuditSwitchAsync(some));
			await Test15((opt, some) => opt.AsTask.AuditSwitchAsync(some, none));
		}

		[Fact]
		public override async Task Test16_None_Runs_None_Action_Catches_Exception_And_Returns_Original_Option()
		{
			var some = Substitute.For<Action<int>>();

			await Test16((opt, none) => opt.AsTask.AuditSwitchAsync(none));
			await Test16((opt, none) => opt.AsTask.AuditSwitchAsync(some, none));
		}

		[Fact]
		public override async Task Test17_None_Runs_None_Func_Catches_Exception_And_Returns_Original_Option()
		{
			var some = Substitute.For<Func<int, Task>>();

			await Test17((opt, none) => opt.AsTask.AuditSwitchAsync(none));
			await Test17((opt, none) => opt.AsTask.AuditSwitchAsync(some, none));
		}

		#endregion

		#region Unused

		public override Task Test00_Null_Args_Returns_Original_Option() =>
			Task.CompletedTask;

		#endregion
	}
}

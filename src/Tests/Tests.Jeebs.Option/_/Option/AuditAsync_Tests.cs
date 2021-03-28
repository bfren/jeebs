// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.Option_Tests
{
	public class AuditAsync_Tests : Jeebs_Tests.AuditAsync_Tests
	{
		#region General

		[Fact]
		public override async Task Test01_If_Unknown_Option_Throws_UnknownOptionException()
		{
			var any = Substitute.For<Func<Option<int>, Task>>();
			var some = Substitute.For<Func<int, Task>>();
			var none = Substitute.For<Func<IMsg, Task>>();

			await Test01(opt => opt.AuditAsync(any));
			await Test01(opt => opt.AuditAsync(some));
			await Test01(opt => opt.AuditAsync(none));
			await Test01(opt => opt.AuditAsync(some, none));
		}

		#endregion

		#region Any

		[Fact]
		public override async Task Test04_Some_Runs_Audit_Func_And_Returns_Original_Option()
		{
			await Test04((opt, any) => opt.AuditAsync(any));
		}

		[Fact]
		public override async Task Test05_None_Runs_Audit_Func_And_Returns_Original_Option()
		{
			await Test05((opt, any) => opt.AuditAsync(any));
		}

		[Fact]
		public override async Task Test08_Some_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Option()
		{
			await Test08((opt, any) => opt.AuditAsync(any));
		}

		[Fact]
		public override async Task Test09_None_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Option()
		{
			await Test09((opt, any) => opt.AuditAsync(any));
		}

		#endregion

		#region Some / None

		[Fact]
		public override async Task Test11_Some_Runs_Some_Func_And_Returns_Original_Option()
		{
			var none = Substitute.For<Func<IMsg, Task>>();

			await Test11((opt, some) => opt.AuditAsync(some));
			await Test11((opt, some) => opt.AuditAsync(some, none));
		}

		[Fact]
		public override async Task Test13_None_Runs_None_Func_And_Returns_Original_Option()
		{
			var some = Substitute.For<Func<int, Task>>();

			await Test13((opt, none) => opt.AuditAsync(none));
			await Test13((opt, none) => opt.AuditAsync(some, none));
		}

		[Fact]
		public override async Task Test15_Some_Runs_Some_Func_Catches_Exception_And_Returns_Original_Option()
		{
			var none = Substitute.For<Func<IMsg, Task>>();

			await Test15((opt, some) => opt.AuditAsync(some));
			await Test15((opt, some) => opt.AuditAsync(some, none));
		}

		[Fact]
		public override async Task Test17_None_Runs_None_Func_Catches_Exception_And_Returns_Original_Option()
		{
			var some = Substitute.For<Func<int, Task>>();

			await Test17((opt, none) => opt.AuditAsync(none));
			await Test17((opt, none) => opt.AuditAsync(some, none));
		}

		#endregion

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

		#endregion
	}
}

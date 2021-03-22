// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs.Option_Tests
{
	public class AuditSwitchAsync_Tests : Jeebs_Tests.AuditSwitchAsync_Tests
	{
		[Fact]
		public override async Task Test01_If_Unknown_Option_Throws_UnknownOptionException()
		{
			var some = Substitute.For<Func<int, Task>>();
			var none = Substitute.For<Func<IMsg, Task>>();

			await Test01(opt => opt.AuditSwitchAsync(some));
			await Test01(opt => opt.AuditSwitchAsync(none));
			await Test01(opt => opt.AuditSwitchAsync(some, none));
		}

		[Fact]
		public override async Task Test03_Some_Runs_Some_Func_And_Returns_Original_Option()
		{
			var none = Substitute.For<Func<IMsg, Task>>();

			await Test03((opt, some) => opt.AuditSwitchAsync(some));
			await Test03((opt, some) => opt.AuditSwitchAsync(some, none));
		}

		[Fact]
		public override async Task Test05_None_Runs_None_Func_And_Returns_Original_Option()
		{
			var some = Substitute.For<Func<int, Task>>();

			await Test05((opt, none) => opt.AuditSwitchAsync(none));
			await Test05((opt, none) => opt.AuditSwitchAsync(some, none));
		}

		[Fact]
		public override async Task Test07_Some_Runs_Some_Func_Catches_Exception_And_Returns_Original_Option()
		{
			var none = Substitute.For<Func<IMsg, Task>>();

			await Test07((opt, some) => opt.AuditSwitchAsync(some));
			await Test07((opt, some) => opt.AuditSwitchAsync(some, none));
		}

		[Fact]
		public override async Task Test09_None_Runs_None_Func_Catches_Exception_And_Returns_Original_Option()
		{
			var some = Substitute.For<Func<int, Task>>();

			await Test09((opt, none) => opt.AuditSwitchAsync(none));
			await Test09((opt, none) => opt.AuditSwitchAsync(some, none));
		}

		#region Unused

		public override Task Test00_Null_Args_Returns_Original_Option() =>
			Task.CompletedTask;

		public override Task Test02_Some_Runs_Some_Action_And_Returns_Original_Option() =>
			Task.CompletedTask;

		public override Task Test04_None_Runs_None_Action_And_Returns_Original_Option() =>
			Task.CompletedTask;

		public override Task Test06_Some_Runs_Some_Action_Catches_Exception_And_Returns_Original_Option() =>
			Task.CompletedTask;

		public override Task Test08_None_Runs_None_Action_Catches_Exception_And_Returns_Original_Option() =>
			Task.CompletedTask;

		#endregion
	}
}

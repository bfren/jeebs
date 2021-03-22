// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.OptionExtensions_Tests
{
	public class AuditSwitchAsync_Tests : Jeebs_Tests.AuditSwitchAsync_Tests
	{
		[Fact]
		public override async Task Test01_If_Unknown_Option_Throws_UnknownOptionException()
		{
			var someA = Substitute.For<Action<int>>();
			var someF = Substitute.For<Func<int, Task>>();
			var noneA = Substitute.For<Action<IMsg>>();
			var noneF = Substitute.For<Func<IMsg, Task>>();

			await Test01(opt => opt.AsTask.AuditSwitchAsync(someA));
			await Test01(opt => opt.AsTask.AuditSwitchAsync(someF));
			await Test01(opt => opt.AsTask.AuditSwitchAsync(noneA));
			await Test01(opt => opt.AsTask.AuditSwitchAsync(noneF));
			await Test01(opt => opt.AsTask.AuditSwitchAsync(someA, noneA));
			await Test01(opt => opt.AsTask.AuditSwitchAsync(someF, noneF));
		}

		[Fact]
		public override async Task Test02_Some_Runs_Some_Action_And_Returns_Original_Option()
		{
			var none = Substitute.For<Action<IMsg>>();

			await Test02((opt, some) => opt.AsTask.AuditSwitchAsync(some));
			await Test02((opt, some) => opt.AsTask.AuditSwitchAsync(some, none));
		}

		[Fact]
		public override async Task Test03_Some_Runs_Some_Func_And_Returns_Original_Option()
		{
			var none = Substitute.For<Func<IMsg, Task>>();

			await Test03((opt, some) => opt.AsTask.AuditSwitchAsync(some));
			await Test03((opt, some) => opt.AsTask.AuditSwitchAsync(some, none));
		}

		[Fact]
		public override async Task Test04_None_Runs_None_Action_And_Returns_Original_Option()
		{
			var some = Substitute.For<Action<int>>();

			await Test04((opt, none) => opt.AsTask.AuditSwitchAsync(none));
			await Test04((opt, none) => opt.AsTask.AuditSwitchAsync(some, none));
		}

		[Fact]
		public override async Task Test05_None_Runs_None_Func_And_Returns_Original_Option()
		{
			var some = Substitute.For<Func<int, Task>>();

			await Test05((opt, none) => opt.AsTask.AuditSwitchAsync(none));
			await Test05((opt, none) => opt.AsTask.AuditSwitchAsync(some, none));
		}

		[Fact]
		public override async Task Test06_Some_Runs_Some_Action_Catches_Exception_And_Returns_Original_Option()
		{
			var none = Substitute.For<Action<IMsg>>();

			await Test06((opt, some) => opt.AsTask.AuditSwitchAsync(some));
			await Test06((opt, some) => opt.AsTask.AuditSwitchAsync(some, none));
		}

		[Fact]
		public override async Task Test07_Some_Runs_Some_Func_Catches_Exception_And_Returns_Original_Option()
		{
			var none = Substitute.For<Func<IMsg, Task>>();

			await Test07((opt, some) => opt.AsTask.AuditSwitchAsync(some));
			await Test07((opt, some) => opt.AsTask.AuditSwitchAsync(some, none));
		}

		[Fact]
		public override async Task Test08_None_Runs_None_Action_Catches_Exception_And_Returns_Original_Option()
		{
			var some = Substitute.For<Action<int>>();

			await Test08((opt, none) => opt.AsTask.AuditSwitchAsync(none));
			await Test08((opt, none) => opt.AsTask.AuditSwitchAsync(some, none));
		}

		[Fact]
		public override async Task Test09_None_Runs_None_Func_Catches_Exception_And_Returns_Original_Option()
		{
			var some = Substitute.For<Func<int, Task>>();

			await Test09((opt, none) => opt.AsTask.AuditSwitchAsync(none));
			await Test09((opt, none) => opt.AsTask.AuditSwitchAsync(some, none));
		}

		#region Unused

		public override Task Test00_Null_Args_Returns_Original_Option() =>
			Task.CompletedTask;

		#endregion
	}
}

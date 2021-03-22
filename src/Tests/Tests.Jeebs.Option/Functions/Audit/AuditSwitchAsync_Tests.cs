// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class AuditSwitchAsync_Tests : Jeebs_Tests.AuditSwitchAsync_Tests
	{
		[Fact]
		public override async Task Test00_Null_Args_Returns_Original_Option()
		{
			await Test00(opt => AuditSwitchAsync(opt, null, null));
			await Test00(opt => AuditSwitchAsync(opt.AsTask, null, null));
		}

		[Fact]
		public override async Task Test01_If_Unknown_Option_Throws_UnknownOptionException()
		{
			var some = Substitute.For<Func<int, Task>>();
			var none = Substitute.For<Func<IMsg, Task>>();

			await Test01(opt => AuditSwitchAsync(opt, some, null));
			await Test01(opt => AuditSwitchAsync(opt, null, none));
			await Test01(opt => AuditSwitchAsync(opt, some, none));
			await Test01(opt => AuditSwitchAsync(opt.AsTask, some, null));
			await Test01(opt => AuditSwitchAsync(opt.AsTask, null, none));
			await Test01(opt => AuditSwitchAsync(opt.AsTask, some, none));
		}

		[Fact]
		public override async Task Test02_Some_Runs_Some_Action_And_Returns_Original_Option()
		{
			await Test02((opt, some) => AuditSwitchAsync(opt.AsTask, some, null));
		}

		[Fact]
		public override async Task Test03_Some_Runs_Some_Func_And_Returns_Original_Option()
		{
			await Test03((opt, some) => AuditSwitchAsync(opt, some, null));
			await Test03((opt, some) => AuditSwitchAsync(opt.AsTask, some, null));
		}

		[Fact]
		public override async Task Test04_None_Runs_None_Action_And_Returns_Original_Option()
		{
			await Test04((opt, none) => AuditSwitchAsync(opt.AsTask, null, none));
		}

		[Fact]
		public override async Task Test05_None_Runs_None_Func_And_Returns_Original_Option()
		{
			await Test05((opt, none) => AuditSwitchAsync(opt, null, none));
			await Test05((opt, none) => AuditSwitchAsync(opt.AsTask, null, none));
		}

		[Fact]
		public override async Task Test06_Some_Runs_Some_Action_Catches_Exception_And_Returns_Original_Option()
		{
			await Test06((opt, some) => AuditSwitchAsync(opt.AsTask, some, null));
		}

		[Fact]
		public override async Task Test07_Some_Runs_Some_Func_Catches_Exception_And_Returns_Original_Option()
		{
			await Test07((opt, some) => AuditSwitchAsync(opt, some, null));
			await Test07((opt, some) => AuditSwitchAsync(opt.AsTask, some, null));
		}

		[Fact]
		public override async Task Test08_None_Runs_None_Action_Catches_Exception_And_Returns_Original_Option()
		{
			await Test08((opt, none) => AuditSwitchAsync(opt.AsTask, null, none));
		}

		[Fact]
		public override async Task Test09_None_Runs_None_Func_Catches_Exception_And_Returns_Original_Option()
		{
			await Test09((opt, none) => AuditSwitchAsync(opt, null, none));
			await Test09((opt, none) => AuditSwitchAsync(opt.AsTask, null, none));
		}
	}
}

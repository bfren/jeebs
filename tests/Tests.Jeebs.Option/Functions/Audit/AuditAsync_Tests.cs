// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class AuditAsync_Tests : Jeebs_Tests.AuditAsync_Tests
	{
		#region General

		[Fact]
		public override async Task Test00_Null_Args_Returns_Original_Option()
		{
			await Test00(opt => AuditAsync(opt, null, null, null));
			await Test00(opt => AuditAsync(opt.AsTask, null, null, null));
		}

		[Fact]
		public override async Task Test01_If_Unknown_Option_Throws_UnknownOptionException()
		{
			var any = Substitute.For<Func<Option<int>, Task>>();
			var some = Substitute.For<Func<int, Task>>();
			var none = Substitute.For<Func<IMsg, Task>>();

			await Test01(opt => AuditAsync(opt, any, null, null));
			await Test01(opt => AuditAsync(opt, null, some, null));
			await Test01(opt => AuditAsync(opt, null, null, none));
			await Test01(opt => AuditAsync(opt, any, some, none));
			await Test01(opt => AuditAsync(opt.AsTask, any, null, null));
			await Test01(opt => AuditAsync(opt.AsTask, null, some, null));
			await Test01(opt => AuditAsync(opt.AsTask, null, null, none));
			await Test01(opt => AuditAsync(opt.AsTask, any, some, none));
		}

		#endregion

		#region Any

		[Fact]
		public override async Task Test02_Some_Runs_Audit_Action_And_Returns_Original_Option()
		{
			await Test02((some, any) => AuditAsync(some.AsTask, any, null, null));
		}

		[Fact]
		public override async Task Test03_None_Runs_Audit_Action_And_Returns_Original_Option()
		{
			await Test03((none, any) => AuditAsync(none.AsTask, any, null, null));
		}

		[Fact]
		public override async Task Test04_Some_Runs_Audit_Func_And_Returns_Original_Option()
		{
			await Test04((some, any) => AuditAsync(some, any, null, null));
			await Test04((some, any) => AuditAsync(some.AsTask, any, null, null));
		}

		[Fact]
		public override async Task Test05_None_Runs_Audit_Func_And_Returns_Original_Option()
		{
			await Test05((none, any) => AuditAsync(none, any, null, null));
			await Test05((none, any) => AuditAsync(none.AsTask, any, null, null));
		}

		[Fact]
		public override async Task Test06_Some_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Option()
		{
			await Test06((some, any) => AuditAsync(some.AsTask, any, null, null));
		}

		[Fact]
		public override async Task Test07_None_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Option()
		{
			await Test07((none, any) => AuditAsync(none.AsTask, any, null, null));
		}

		[Fact]
		public override async Task Test08_Some_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Option()
		{
			await Test08((some, any) => AuditAsync(some, any, null, null));
			await Test08((some, any) => AuditAsync(some.AsTask, any, null, null));
		}

		[Fact]
		public override async Task Test09_None_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Option()
		{
			await Test09((none, any) => AuditAsync(none, any, null, null));
			await Test05((none, any) => AuditAsync(none.AsTask, any, null, null));
		}

		#endregion

		#region Some / None
		[Fact]
		public override async Task Test10_Some_Runs_Some_Action_And_Returns_Original_Option()
		{
			await Test10((opt, some) => AuditAsync(opt.AsTask, null, some, null));
		}

		[Fact]
		public override async Task Test11_Some_Runs_Some_Func_And_Returns_Original_Option()
		{
			await Test11((opt, some) => AuditAsync(opt, null, some, null));
			await Test11((opt, some) => AuditAsync(opt.AsTask, null, some, null));
		}

		[Fact]
		public override async Task Test12_None_Runs_None_Action_And_Returns_Original_Option()
		{
			await Test12((opt, none) => AuditAsync(opt.AsTask, null, null, none));
		}

		[Fact]
		public override async Task Test13_None_Runs_None_Func_And_Returns_Original_Option()
		{
			await Test13((opt, none) => AuditAsync(opt, null, null, none));
			await Test13((opt, none) => AuditAsync(opt.AsTask, null, null, none));
		}

		[Fact]
		public override async Task Test14_Some_Runs_Some_Action_Catches_Exception_And_Returns_Original_Option()
		{
			await Test14((opt, some) => AuditAsync(opt.AsTask, null, some, null));
		}

		[Fact]
		public override async Task Test15_Some_Runs_Some_Func_Catches_Exception_And_Returns_Original_Option()
		{
			await Test15((opt, some) => AuditAsync(opt, null, some, null));
			await Test15((opt, some) => AuditAsync(opt.AsTask, null, some, null));
		}

		[Fact]
		public override async Task Test16_None_Runs_None_Action_Catches_Exception_And_Returns_Original_Option()
		{
			await Test16((opt, none) => AuditAsync(opt.AsTask, null, null, none));
		}

		[Fact]
		public override async Task Test17_None_Runs_None_Func_Catches_Exception_And_Returns_Original_Option()
		{
			await Test17((opt, none) => AuditAsync(opt, null, null, none));
			await Test17((opt, none) => AuditAsync(opt.AsTask, null, null, none));
		}

		#endregion
	}
}

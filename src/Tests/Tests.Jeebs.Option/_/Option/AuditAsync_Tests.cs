// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Xunit;

namespace Jeebs.Option_Tests
{
	public class AuditAsync_Tests : Jeebs_Tests.AuditAsync_Tests
	{
		[Fact]
		public override async Task Test02_Some_Runs_Audit_Func_And_Returns_Original_Option()
		{
			await Test02((opt, audit) => opt.AuditAsync(audit));
		}

		[Fact]
		public override async Task Test03_None_Runs_Audit_Func_And_Returns_Original_Option()
		{
			await Test03((opt, audit) => opt.AuditAsync(audit));
		}

		[Fact]
		public override async Task Test06_Some_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Option()
		{
			await Test06((opt, audit) => opt.AuditAsync(audit));
		}

		[Fact]
		public override async Task Test07_None_Runs_Audit_Func_Catches_Exception_And_Returns_Original_Option()
		{
			await Test07((opt, audit) => opt.AuditAsync(audit));
		}

		#region Unused

		public override Task Test00_Some_Runs_Audit_Action_And_Returns_Original_Option() =>
			Task.CompletedTask;

		public override Task Test01_None_Runs_Audit_Action_And_Returns_Original_Option() =>
			Task.CompletedTask;

		public override Task Test04_Some_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Option() =>
			Task.CompletedTask;

		public override Task Test05_None_Runs_Audit_Action_Catches_Exception_And_Returns_Original_Option() =>
			Task.CompletedTask;

		#endregion
	}
}

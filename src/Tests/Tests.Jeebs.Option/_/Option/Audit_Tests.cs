// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Option_Tests
{
	public class Audit_Tests : Jeebs_Tests.Audit_Tests
	{
		[Fact]
		public override void Test00_Some_Runs_Audit_And_Returns_Original_Option()
		{
			Test00((opt, audit) => opt.Audit(audit));
		}

		[Fact]
		public override void Test01_None_Runs_Audit_And_Returns_Original_Option()
		{
			Test01((opt, audit) => opt.Audit(audit));
		}

		[Fact]
		public override void Test02_Some_Catches_Exception_And_Returns_Original_Option()
		{
			Test02((opt, audit) => opt.Audit(audit));
		}

		[Fact]
		public override void Test03_None_Catches_Exception_And_Returns_Original_Option()
		{
			Test03((opt, audit) => opt.Audit(audit));
		}
	}
}

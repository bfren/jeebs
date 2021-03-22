// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class AuditSwitch_Tests : Jeebs_Tests.AuditSwitch_Tests
	{
		[Fact]
		public override void Test00_Null_Args_Returns_Original_Option()
		{
			Test00(opt => AuditSwitch(opt, null, null));
		}

		[Fact]
		public override void Test01_If_Unknown_Option_Throws_UnknownOptionException()
		{
			Test01(opt => AuditSwitch(opt, Substitute.For<Action<int>>(), null));
			Test01(opt => AuditSwitch(opt, null, Substitute.For<Action<IMsg>>()));
		}

		[Fact]
		public override void Test02_Some_Runs_Some_And_Returns_Original_Option()
		{
			Test02((opt, some) => AuditSwitch(opt, some, null));
		}

		[Fact]
		public override void Test03_None_Runs_None_And_Returns_Original_Option()
		{
			Test03((opt, none) => AuditSwitch(opt, null, none));
		}

		[Fact]
		public override void Test04_Some_Catches_Exception_And_Returns_Original_Option()
		{
			Test04((opt, some) => AuditSwitch(opt, some, null));
		}

		[Fact]
		public override void Test05_None_Catches_Exception_And_Returns_Original_Option()
		{
			Test05((opt, none) => AuditSwitch(opt, null, none));
		}
	}
}

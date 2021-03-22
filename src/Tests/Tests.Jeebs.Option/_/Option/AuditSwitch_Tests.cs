// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Exceptions;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace Jeebs.Option_Tests
{
	public class AuditSwitch_Tests : Jeebs_Tests.AuditSwitch_Tests
	{
		[Fact]
		public override void Test01_If_Unknown_Option_Throws_UnknownOptionException()
		{
			Test01(opt => opt.AuditSwitch(Substitute.For<Action<int>>()));
			Test01(opt => opt.AuditSwitch(Substitute.For<Action<IMsg>>()));
			Test01(opt => opt.AuditSwitch(Substitute.For<Action<int>>(), Substitute.For<Action<IMsg>>()));
		}

		[Fact]
		public override void Test02_Some_Runs_Some_And_Returns_Original_Option()
		{
			Test02((opt, some) => opt.AuditSwitch(some));
			Test02((opt, some) => opt.AuditSwitch(some, Substitute.For<Action<IMsg>>()));
		}

		[Fact]

		public override void Test03_None_Runs_None_And_Returns_Original_Option()
		{
			Test03((opt, none) => opt.AuditSwitch(none));
			Test03((opt, none) => opt.AuditSwitch(Substitute.For<Action<int>>(), none));
		}

		[Fact]
		public override void Test04_Some_Catches_Exception_And_Returns_Original_Option()
		{
			Test04((opt, some) => opt.AuditSwitch(some));
			Test04((opt, some) => opt.AuditSwitch(some, Substitute.For<Action<IMsg>>()));
		}

		[Fact]
		public override void Test05_None_Catches_Exception_And_Returns_Original_Option()
		{
			Test05((opt, none) => opt.AuditSwitch(none));
			Test05((opt, none) => opt.AuditSwitch(Substitute.For<Action<int>>(), none));
		}

		#region Unused

		[Fact]
		public override void Test00_Null_Args_Returns_Original_Option() { }

		#endregion
	}
}

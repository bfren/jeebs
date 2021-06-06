// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class Audit_Tests : Jeebs_Tests.Audit_Tests
	{
		#region General

		[Fact]
		public override void Test00_Null_Args_Returns_Original_Option()
		{
			Test00(opt => Audit(opt, null, null, null));
		}

		[Fact]
		public override void Test01_If_Unknown_Option_Throws_UnknownOptionException()
		{
			Test01(opt => Audit(opt, Substitute.For<Action<Option<int>>>(), null, null));
			Test01(opt => Audit(opt, null, Substitute.For<Action<int>>(), null));
			Test01(opt => Audit(opt, null, null, Substitute.For<Action<IMsg>>()));
		}

		#endregion

		#region Any

		[Fact]
		public override void Test02_Some_Runs_Audit_And_Returns_Original_Option()
		{
			Test02((opt, any) => Audit(opt, any, null, null));
		}

		[Fact]
		public override void Test03_None_Runs_Audit_And_Returns_Original_Option()
		{
			Test03((opt, any) => Audit(opt, any, null, null));
		}

		[Fact]
		public override void Test04_Some_Catches_Exception_And_Returns_Original_Option()
		{
			Test04((opt, any) => Audit(opt, any, null, null));
		}

		[Fact]
		public override void Test05_None_Catches_Exception_And_Returns_Original_Option()
		{
			Test05((opt, any) => Audit(opt, any, null, null));
		}

		#endregion

		#region Some / None

		[Fact]
		public override void Test06_Some_Runs_Some_And_Returns_Original_Option()
		{
			Test06((opt, some) => Audit(opt, null, some, null));
		}

		[Fact]
		public override void Test07_None_Runs_None_And_Returns_Original_Option()
		{
			Test07((opt, none) => Audit(opt, null, null, none));
		}

		[Fact]
		public override void Test08_Some_Catches_Exception_And_Returns_Original_Option()
		{
			Test08((opt, some) => Audit(opt, null, some, null));
		}

		[Fact]
		public override void Test09_None_Catches_Exception_And_Returns_Original_Option()
		{
			Test09((opt, none) => Audit(opt, null, null, none));
		}

		#endregion
	}
}

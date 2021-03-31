// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class SwitchIf_Tests : Jeebs_Tests.SwitchIf_Tests
	{
		[Fact]
		public override void Test00_If_Unknown_Option_Throws_UnknownOptionException()
		{
			Test00((opt, check, ifFalse) => SwitchIf(opt, check, ifFalse));
		}

		[Fact]
		public override void Test01_Check_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
		{
			Test01((opt, check, ifFalse) => SwitchIf(opt, check, ifFalse));
		}

		[Fact]
		public override void Test02_IfFalse_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
		{
			Test02((opt, check, ifFalse) => SwitchIf(opt, check, ifFalse));
		}

		[Fact]
		public override void Test03_If_None_Returns_Original_None()
		{
			Test03((opt, check, ifFalse) => SwitchIf(opt, check, ifFalse));
		}

		[Fact]
		public override void Test04_If_Some_And_Check_Is_False_Runs_IfFalse_Returns_None()
		{
			Test04((opt, check, ifFalse) => SwitchIf(opt, check, ifFalse));
		}

		[Fact]
		public override void Test05_If_Some_And_Check_Is_True_Returns_Original_Some()
		{
			Test05((opt, check, ifFalse) => SwitchIf(opt, check, ifFalse));
		}
	}
}

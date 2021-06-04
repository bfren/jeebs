// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class IfSome_Tests : Jeebs_Tests.IfSome_Tests
	{
		[Fact]
		public override void Test00_Exception_In_IfSome_Action_Returns_None_With_UnhandledExceptionMsg()
		{
			Test00((opt, ifSome) => IfSome(opt, ifSome));
		}

		[Fact]
		public override void Test01_None_Returns_Original_Option()
		{
			Test01((opt, ifSome) => IfSome(opt, ifSome));
		}

		[Fact]
		public override void Test02_Some_Runs_IfSome_Action_And_Returns_Original_Option()
		{
			Test02((opt, ifSome) => IfSome(opt, ifSome));
		}
	}
}
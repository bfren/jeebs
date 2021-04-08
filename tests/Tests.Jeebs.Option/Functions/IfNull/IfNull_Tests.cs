// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class IfNull_Tests : Jeebs_Tests.IfNull_Tests
	{
		[Fact]
		public override void Test00_Exception_In_IfNull_Func_Returns_None_With_UnhandledExceptionMsg()
		{
			Test00((opt, ifNull) => IfNull(opt, ifNull));
			Test00((opt, ifNull) => IfNull(opt, () => { ifNull(); return new TestMsg(); }));
		}

		[Fact]
		public override void Test01_Some_With_Null_Value_Runs_IfNull_Func()
		{
			Test01((opt, ifNull) => IfNull(opt, ifNull));
		}

		[Fact]
		public override void Test02_None_With_NullValueMsg_Runs_IfNull_Func()
		{
			Test02((opt, ifNull) => IfNull(opt, ifNull));
		}

		[Fact]
		public override void Test03_Some_With_Null_Value_Runs_IfNull_Func_Returns_None_With_Reason()
		{
			Test03((opt, ifNull) => IfNull(opt, ifNull));
		}

		[Fact]
		public override void Test04_None_With_NullValueMsg_Runs_IfNull_Func_Returns_None_With_Reason()
		{
			Test04((opt, ifNull) => IfNull(opt, ifNull));
		}
	}
}
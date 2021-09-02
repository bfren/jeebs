// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Option_Tests
{
	public class IfNull_Tests : Jeebs_Tests.IfNull_Tests
	{
		[Fact]
		public override void Test00_Exception_In_IfNull_Func_Returns_None_With_UnhandledExceptionMsg()
		{
			Test00((opt, ifNull) => opt.IfNull(ifNull));
			Test00((opt, ifNull) => opt.IfNull(() => { ifNull(); return new TestMsg(); }));
		}

		[Fact]
		public override void Test01_Some_With_Null_Value_Runs_IfNull_Func()
		{
			Test01((opt, ifNull) => opt.IfNull(ifNull));
		}

		[Fact]
		public override void Test02_None_With_NullValueMsg_Runs_IfNull_Func()
		{
			Test02((opt, ifNull) => opt.IfNull(ifNull));
		}

		[Fact]
		public override void Test03_Some_With_Null_Value_Runs_IfNull_Func_Returns_None_With_Reason()
		{
			Test03((opt, ifNull) => opt.IfNull(ifNull));
		}

		[Fact]
		public override void Test04_None_With_NullValueMsg_Runs_IfNull_Func_Returns_None_With_Reason()
		{
			Test04((opt, ifNull) => opt.IfNull(ifNull));
		}
	}
}
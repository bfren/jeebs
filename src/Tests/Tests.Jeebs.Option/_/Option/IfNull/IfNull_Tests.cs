// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;

namespace Jeebs.Option_Tests
{
	public class IfNull_Tests : Jeebs_Tests.IfNull_Tests
	{
		[Fact]
		public override void Test00_Exception_In_NullValue_Func_Returns_None_With_UnhandledExceptionMsg()
		{
			Test00((opt, nullValue) => opt.IfNull(nullValue));
		}

		[Fact]
		public override void Test01_Some_With_Null_Value_Runs_NullValue_Func()
		{
			Test01((opt, nullValue) => opt.IfNull(nullValue));
		}

		[Fact]
		public override void Test02_None_With_NullValueMsg_Runs_NullValue_Func()
		{
			Test02((opt, nullValue) => opt.IfNull(nullValue));
		}
	}
}
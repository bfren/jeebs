// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System.Threading.Tasks;
using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class IfNullAsync_Tests : Jeebs_Tests.IfNullAsync_Tests
	{
		[Fact]
		public override async Task Test00_Exception_In_NullValue_Func_Returns_None_With_UnhandledExceptionMsg()
		{
			await Test00((opt, ifNull) => IfNullAsync(opt, ifNull));
			await Test00((opt, ifNull) => IfNullAsync(opt.AsTask, ifNull));
			await Test00((opt, ifNull) => IfNullAsync(opt.AsTask, () => { ifNull(); return new TestMsg(); }));
		}

		[Fact]
		public override async Task Test01_Some_With_Null_Value_Runs_IfNull_Func()
		{
			await Test01((opt, ifNull) => IfNullAsync(opt, ifNull));
			await Test01((opt, ifNull) => IfNullAsync(opt.AsTask, ifNull));
		}

		[Fact]
		public override async Task Test02_None_With_NullValueMsg_Runs_IfNull_Func()
		{
			await Test02((opt, ifNull) => IfNullAsync(opt, ifNull));
			await Test02((opt, ifNull) => IfNullAsync(opt.AsTask, ifNull));
		}

		[Fact]
		public override async Task Test03_Some_With_Null_Value_Runs_IfNull_Func_Returns_None_With_Reason()
		{
			await Test03((opt, ifNull) => IfNullAsync(opt.AsTask, ifNull));
		}

		[Fact]
		public override async Task Test04_None_With_NullValueMsg_Runs_IfNull_Func_Returns_None_With_Reason()
		{
			await Test04((opt, ifNull) => IfNullAsync(opt.AsTask, ifNull));
		}
	}
}
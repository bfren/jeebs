// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.OptionExtensions_Tests
{
	public class IfNullAsync_Tests : Jeebs_Tests.IfNullAsync_Tests
	{
		[Fact]
		public override async Task Test00_Exception_In_NullValue_Func_Returns_None_With_UnhandledExceptionMsg()
		{
			await Test00((opt, ifNull) => opt.AsTask.IfNullAsync(ifNull));
			await Test00((opt, ifNull) => opt.AsTask.IfNullAsync(() => ifNull().GetAwaiter().GetResult()));
			await Test00((opt, ifNull) => opt.AsTask.IfNullAsync(() => { ifNull(); return new TestMsg(); }));
		}

		[Fact]
		public override async Task Test01_Some_With_Null_Value_Runs_IfNull_Func()
		{
			await Test01((opt, ifNull) => opt.AsTask.IfNullAsync(ifNull));
			await Test01((opt, ifNull) => opt.AsTask.IfNullAsync(() => ifNull().GetAwaiter().GetResult()));
		}

		[Fact]
		public override async Task Test02_None_With_NullValueMsg_Runs_IfNull_Func()
		{
			await Test02((opt, ifNull) => opt.AsTask.IfNullAsync(ifNull));
			await Test02((opt, ifNull) => opt.AsTask.IfNullAsync(() => ifNull().GetAwaiter().GetResult()));
		}

		[Fact]
		public override async Task Test03_Some_With_Null_Value_Runs_IfNull_Func_Returns_None_With_Reason()
		{
			await Test03((opt, ifNull) => opt.AsTask.IfNullAsync(ifNull));
		}

		[Fact]
		public override async Task Test04_None_With_NullValueMsg_Runs_IfNull_Func_Returns_None_With_Reason()
		{
			await Test04((opt, ifNull) => opt.AsTask.IfNullAsync(ifNull));
		}
	}
}
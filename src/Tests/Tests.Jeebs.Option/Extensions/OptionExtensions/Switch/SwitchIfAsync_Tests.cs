// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System.Threading.Tasks;
using Xunit;

namespace Jeebs.OptionExtensions_Tests
{
	public class SwitchIfAsync_Tests : Jeebs_Tests.SwitchIfAsync_Tests
	{
		[Fact]
		public override async Task Test00_If_Unknown_Option_Throws_UnknownOptionException()
		{
			await Test00((opt, check, ifFalse) => opt.SwitchIfAsync(check, ifFalse));
			await Test00((opt, check, ifFalse) => opt.SwitchIfAsync(check, x => ifFalse(x).Reason));
		}

		[Fact]
		public override async Task Test01_Check_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
		{
			await Test01((opt, check, ifFalse) => opt.SwitchIfAsync(check, ifFalse));
			await Test01((opt, check, ifFalse) => opt.SwitchIfAsync(check, x => ifFalse(x).Reason));
		}

		[Fact]
		public override async Task Test02_IfFalse_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
		{
			await Test02((opt, check, ifFalse) => opt.SwitchIfAsync(check, ifFalse));
			await Test02((opt, check, ifFalse) => opt.SwitchIfAsync(check, x => ifFalse(x).Reason));
		}

		[Fact]
		public override async Task Test03_If_None_Returns_Original_None()
		{
			await Test03((opt, check, ifFalse) => opt.SwitchIfAsync(check, ifFalse));
			await Test03((opt, check, ifFalse) => opt.SwitchIfAsync(check, x => ifFalse(x).Reason));
		}

		[Fact]
		public override async Task Test04_If_Some_And_Check_Is_False_Runs_IfFalse_Returns_None()
		{
			await Test04((opt, check, ifFalse) => opt.SwitchIfAsync(check, ifFalse));
			await Test04((opt, check, ifFalse) => opt.SwitchIfAsync(check, x => ifFalse(x).Reason));
		}

		[Fact]
		public override async Task Test05_If_Some_And_Check_Is_True_Returns_Original_Some()
		{
			await Test05((opt, check, ifFalse) => opt.SwitchIfAsync(check, ifFalse));
			await Test05((opt, check, ifFalse) => opt.SwitchIfAsync(check, x => ifFalse(x).Reason));
		}
	}
}

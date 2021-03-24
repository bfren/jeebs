﻿// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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
		}

		[Fact]
		public override async Task Test01_Check_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
		{
			await Test01((opt, check, ifFalse) => opt.SwitchIfAsync(check, ifFalse));
		}

		[Fact]
		public override async Task Test02_IfFalse_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
		{
			await Test02((opt, check, ifFalse) => opt.SwitchIfAsync(check, ifFalse));
		}

		[Fact]
		public override async Task Test03_If_None_Returns_Original_None()
		{
			await Test03((opt, check, ifFalse) => opt.SwitchIfAsync(check, ifFalse));
		}

		[Fact]
		public override async Task Test04_If_Some_And_Check_Is_False_Runs_IfFalse_Returns_None()
		{
			await Test04((opt, check, ifFalse) => opt.SwitchIfAsync(check, ifFalse));
		}

		[Fact]
		public override async Task Test05_If_Some_And_Check_Is_True_Returns_Original_Some()
		{
			await Test05((opt, check, ifFalse) => opt.SwitchIfAsync(check, ifFalse));
		}
	}
}
// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using System.Threading.Tasks;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class UnwrapAsync_Tests : Jeebs_Tests.UnwrapAsync_Tests
	{
		[Fact]
		public override async Task Test00_None_Runs_IfNone_Func_Returns_Value()
		{
			await Test00((opt, ifNone) => UnwrapAsync(opt, x => x.Value(ifNone())));
			await Test00((opt, ifNone) => UnwrapAsync(opt, x => x.Value(ifNone)));
		}

		[Fact]
		public override async Task Test01_None_With_Reason_Runs_IfNone_Func_Passes_Reason_Returns_Value()
		{
			await Test01((opt, ifNone) => UnwrapAsync(opt, x => x.Value(ifNone)));
		}

		[Fact]
		public override async Task Test02_Some_Returns_Value()
		{
			await Test02(opt => UnwrapAsync(opt, x => x.Value(Rnd.Int)));
			await Test02(opt => UnwrapAsync(opt, x => x.Value(Substitute.For<Func<int>>())));
			await Test02(opt => UnwrapAsync(opt, x => x.Value(Substitute.For<Func<IMsg, int>>())));
		}
	}
}

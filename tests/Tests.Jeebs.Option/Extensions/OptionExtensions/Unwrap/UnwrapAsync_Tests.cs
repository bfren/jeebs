// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using System.Threading.Tasks;
using NSubstitute;
using Xunit;

namespace Jeebs.OptionExtensions_Tests
{
	public class UnwrapAsync_Tests : Jeebs_Tests.UnwrapAsync_Tests
	{
		[Fact]
		public override async Task Test00_None_Runs_IfNone_Func_Returns_Value()
		{
			await Test00((opt, ifNone) => opt.UnwrapAsync(x => x.Value(ifNone())));
			await Test00((opt, ifNone) => opt.UnwrapAsync(x => x.Value(ifNone)));
		}

		[Fact]
		public override async Task Test01_None_With_Reason_Runs_IfNone_Func_Passes_Reason_Returns_Value()
		{
			await Test01((opt, ifNone) => opt.UnwrapAsync(x => x.Value(ifNone)));
		}

		[Fact]
		public override async Task Test02_Some_Returns_Value()
		{
			await Test02(opt => opt.UnwrapAsync(x => x.Value(F.Rnd.Int)));
			await Test02(opt => opt.UnwrapAsync(x => x.Value(Substitute.For<Func<int>>())));
			await Test02(opt => opt.UnwrapAsync(x => x.Value(Substitute.For<Func<IMsg, int>>())));
		}
	}
}

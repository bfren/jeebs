// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests
{
	public class Unwrap_Tests : Jeebs_Tests.Unwrap_Tests
	{
		[Fact]
		public override void Test00_None_Runs_IfNone_Func_Returns_Value()
		{
			Test00((opt, ifNone) => Unwrap(opt, ifNone));
		}

		[Fact]
		public override void Test01_None_With_Reason_Runs_IfNone_Func_Passes_Reason_Returns_Value()
		{
			Test01((opt, ifNone) => Unwrap(opt, ifNone));
		}

		[Fact]
		public override void Test02_Some_Returns_Value()
		{
			Test02(opt => Unwrap(opt, Substitute.For<Func<int>>()));
			Test02(opt => Unwrap(opt, Substitute.For<Func<IMsg, int>>()));
		}
	}
}

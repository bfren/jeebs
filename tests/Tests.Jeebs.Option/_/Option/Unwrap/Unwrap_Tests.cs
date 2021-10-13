// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using NSubstitute;
using Xunit;

namespace Jeebs.Option_Tests;

public class Unwrap_Tests : Jeebs_Tests.Unwrap_Tests
{
	[Fact]
	public override void Test00_None_Runs_IfNone_Func_Returns_Value()
	{
		Test00((opt, ifNone) => opt.Unwrap(ifNone));
	}

	[Fact]
	public override void Test01_None_With_Reason_Runs_IfNone_Func_Passes_Reason_Returns_Value()
	{
		Test01((opt, ifNone) => opt.Unwrap(ifNone));
	}

	[Fact]
	public override void Test02_Some_Returns_Value()
	{
		Test02(opt => opt.Unwrap(Substitute.For<Func<int>>()));
		Test02(opt => opt.Unwrap(Substitute.For<Func<IMsg, int>>()));
	}
}

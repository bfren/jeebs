﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.MaybeF;

namespace F.MaybeF_Tests;

public class Unwrap_Tests : Jeebs_Tests.Unwrap_Tests
{
	[Fact]
	public override void Test00_None_Runs_IfNone_Func_Returns_Value()
	{
		Test00((mbe, ifNone) => Unwrap(mbe, ifNone));
	}

	[Fact]
	public override void Test01_None_With_Reason_Runs_IfNone_Func_Passes_Reason_Returns_Value()
	{
		Test01((mbe, ifNone) => Unwrap(mbe, ifNone));
	}

	[Fact]
	public override void Test02_Some_Returns_Value()
	{
		Test02(mbe => Unwrap(mbe, Substitute.For<Func<int>>()));
		Test02(mbe => Unwrap(mbe, Substitute.For<Func<Msg, int>>()));
	}
}

﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using Jeebs;
using NSubstitute;
using Xunit;
using static F.MaybeF;

namespace F.MaybeF_Tests;

public class SwitchIf_Tests : Jeebs_Tests.SwitchIf_Tests
{
	[Fact]
	public override void Test00_Unknown_Maybe_Throws_UnknownOptionException()
	{
		var ifFalse = Substitute.For<Func<int, Msg>>();
		Test00((mbe, check) => SwitchIf(mbe, check, null, null));
		Test00((mbe, check) => SwitchIf(mbe, check, ifFalse));
	}

	[Fact]
	public override void Test01_None_Returns_Original_None()
	{
		var ifFalse = Substitute.For<Func<int, Msg>>();
		Test01((mbe, check) => SwitchIf(mbe, check, null, null));
		Test01((mbe, check) => SwitchIf(mbe, check, ifFalse));
	}

	[Fact]
	public override void Test02_Check_Func_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
	{
		var ifFalse = Substitute.For<Func<int, Msg>>();
		Test02((mbe, check) => SwitchIf(mbe, check, null, null));
		Test02((mbe, check) => SwitchIf(mbe, check, ifFalse));
	}

	[Fact]
	public override void Test03_Check_Returns_True_And_IfTrue_Is_Null_Returns_Original_Option()
	{
		Test03((mbe, check) => SwitchIf(mbe, check, null, null));
	}

	[Fact]
	public override void Test04_Check_Returns_False_And_IfFalse_Is_Null_Returns_Original_Option()
	{
		Test04((mbe, check) => SwitchIf(mbe, check, null, null));
	}

	[Fact]
	public override void Test05_Check_Returns_True_And_IfTrue_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
	{
		Test05((mbe, check, ifTrue) => SwitchIf(mbe, check, ifTrue, null));
	}

	[Fact]
	public override void Test06_Check_Returns_False_And_IfFalse_Throws_Exception_Returns_None_With_SwitchIfFuncExceptionMsg()
	{
		Test06((mbe, check, ifFalse) => SwitchIf(mbe, check, null, ifFalse));
		Test06((mbe, check, ifFalse) => SwitchIf(mbe, check, x => ifFalse(x).Reason));
	}

	[Fact]
	public override void Test07_Check_Returns_True_Runs_IfTrue_Returns_Value()
	{
		Test07((mbe, check, ifTrue) => SwitchIf(mbe, check, ifTrue, null));
	}

	[Fact]
	public override void Test08_Check_Returns_False_Runs_IfFalse_Returns_Value()
	{
		Test08((mbe, check, ifFalse) => SwitchIf(mbe, check, null, ifFalse));
		Test08((mbe, check, ifFalse) => SwitchIf(mbe, check, x => ifFalse(x).Reason));
	}
}

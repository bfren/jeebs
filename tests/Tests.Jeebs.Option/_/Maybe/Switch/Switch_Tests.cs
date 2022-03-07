﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using NSubstitute;
using Xunit;

namespace Jeebs.Maybe_Tests;

public class Switch_Tests : Jeebs_Tests.Switch_Tests
{
	[Fact]
	public override void Test00_Return_Void_If_Unknown_Maybe_Throws_UnknownOptionException()
	{
		var some = Substitute.For<Action<int>>();
		var none = Substitute.For<Action<Msg>>();
		Test00(mbe => mbe.Switch(some, () => none(new TestMsg())));
		Test00(mbe => mbe.Switch(some, none));
	}

	[Fact]
	public override void Test01_Return_Value_If_Unknown_Maybe_Throws_UnknownOptionException()
	{
		var some = Substitute.For<Func<int, string>>();
		var none = Substitute.For<Func<Msg, string>>();
		Test01(mbe => mbe.Switch(some, none(new TestMsg())));
		Test01(mbe => mbe.Switch(some, () => none(new TestMsg())));
		Test01(mbe => mbe.Switch(some, none));
	}

	[Fact]
	public override void Test02_Return_Void_If_None_Runs_None_Action_With_Reason()
	{
		var some = Substitute.For<Action<int>>();
		Test02((mbe, none) => mbe.Switch(some, () => none(new TestMsg())));
		Test02((mbe, none) => mbe.Switch(some, none));
	}

	[Fact]
	public override void Test03_Return_Value_If_None_Runs_None_Func_With_Reason()
	{
		var some = Substitute.For<Func<int, string>>();
		Test03((mbe, none) => mbe.Switch(some, none(new TestMsg())));
		Test03((mbe, none) => mbe.Switch(some, () => none(new TestMsg())));
		Test03((mbe, none) => mbe.Switch(some, none));
	}

	[Fact]
	public override void Test04_Return_Void_If_Some_Runs_Some_Action_With_Value()
	{
		Test04((mbe, some) => mbe.Switch(some, Substitute.For<Action>()));
		Test04((mbe, some) => mbe.Switch(some, Substitute.For<Action<Msg>>()));
	}

	[Fact]
	public override void Test05_Return_Value_If_Some_Runs_Some_Func_With_Value()
	{
		Test05((mbe, some) => mbe.Switch(some, F.Rnd.Str));
		Test05((mbe, some) => mbe.Switch(some, Substitute.For<Func<string>>()));
		Test05((mbe, some) => mbe.Switch(some, Substitute.For<Func<Msg, string>>()));
	}
}

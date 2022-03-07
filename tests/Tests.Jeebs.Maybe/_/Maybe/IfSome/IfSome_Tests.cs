﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;

namespace Jeebs.Maybe_Tests;

public class IfSome_Tests : Jeebs_Tests.IfSome_Tests
{
	[Fact]
	public override void Test00_Exception_In_IfSome_Action_Returns_None_With_UnhandledExceptionMsg()
	{
		Test00((mbe, ifSome) => mbe.IfSome(ifSome));
	}

	[Fact]
	public override void Test01_None_Returns_Original_Option()
	{
		Test01((mbe, ifSome) => mbe.IfSome(ifSome));
	}

	[Fact]
	public override void Test02_Some_Runs_IfSome_Action_And_Returns_Original_Option()
	{
		Test02((mbe, ifSome) => mbe.IfSome(ifSome));
	}
}
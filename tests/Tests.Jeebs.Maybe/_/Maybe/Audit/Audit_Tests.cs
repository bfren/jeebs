// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using System;
using NSubstitute;
using Xunit;

namespace Jeebs.Maybe_Tests;

public class Audit_Tests : Jeebs_Tests.Audit_Tests
{
	#region General

	[Fact]
	public override void Test01_If_Unknown_Maybe_Throws_UnknownOptionException()
	{
		Test01(mbe => mbe.Audit(Substitute.For<Action<Maybe<int>>>()));
		Test01(mbe => mbe.Audit(Substitute.For<Action<int>>()));
		Test01(mbe => mbe.Audit(Substitute.For<Action<Msg>>()));
		Test01(mbe => mbe.Audit(Substitute.For<Action<int>>(), Substitute.For<Action<Msg>>()));
	}

	#endregion

	#region Any

	[Fact]
	public override void Test02_Some_Runs_Audit_And_Returns_Original_Option()
	{
		Test02((mbe, any) => mbe.Audit(any));
	}

	[Fact]
	public override void Test03_None_Runs_Audit_And_Returns_Original_Option()
	{
		Test03((mbe, any) => mbe.Audit(any));
	}

	[Fact]
	public override void Test04_Some_Catches_Exception_And_Returns_Original_Option()
	{
		Test04((mbe, any) => mbe.Audit(any));
	}

	[Fact]
	public override void Test05_None_Catches_Exception_And_Returns_Original_Option()
	{
		Test05((mbe, any) => mbe.Audit(any));
	}

	#endregion

	#region Some / None

	[Fact]
	public override void Test06_Some_Runs_Some_And_Returns_Original_Option()
	{
		Test06((mbe, some) => mbe.Audit(some));
		Test06((mbe, some) => mbe.Audit(some, Substitute.For<Action<Msg>>()));
	}

	[Fact]

	public override void Test07_None_Runs_None_And_Returns_Original_Option()
	{
		Test07((mbe, none) => mbe.Audit(none));
		Test07((mbe, none) => mbe.Audit(Substitute.For<Action<int>>(), none));
	}

	[Fact]
	public override void Test08_Some_Catches_Exception_And_Returns_Original_Option()
	{
		Test08((mbe, some) => mbe.Audit(some));
		Test08((mbe, some) => mbe.Audit(some, Substitute.For<Action<Msg>>()));
	}

	[Fact]
	public override void Test09_None_Catches_Exception_And_Returns_Original_Option()
	{
		Test09((mbe, none) => mbe.Audit(none));
		Test09((mbe, none) => mbe.Audit(Substitute.For<Action<int>>(), none));
	}

	#endregion

	#region Unused

	[Fact]
	public override void Test00_Null_Args_Returns_Original_Option()
	{
		// Unused
	}

	#endregion
}

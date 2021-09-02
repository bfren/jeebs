// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using NSubstitute;
using Xunit;
using static F.OptionF;

namespace F.OptionF_Tests;

public class Filter_Tests : Jeebs_Tests.Filter_Tests
{
	[Fact]
	public override void Test00_If_Unknown_Option_Returns_None_With_UnhandledExceptionMsg()
	{
		var predicate = Substitute.For<Func<int, bool>>();
		Test00(opt => Filter(opt, predicate));
	}

	[Fact]
	public override void Test01_Exception_Thrown_Returns_None_With_UnhandledExceptionMsg()
	{
		Test01((opt, predicate) => Filter(opt, predicate));
	}

	[Fact]
	public override void Test02_When_Some_And_Predicate_True_Returns_Value()
	{
		Test02((opt, predicate) => Filter(opt, predicate));
	}

	[Fact]
	public override void Test03_When_Some_And_Predicate_False_Returns_None_With_PredicateWasFalseMsg()
	{
		Test03((opt, predicate) => Filter(opt, predicate));
	}

	[Fact]
	public override void Test04_When_None_Returns_None_With_Original_Reason()
	{
		Test04((opt, predicate) => Filter(opt, predicate));
	}
}

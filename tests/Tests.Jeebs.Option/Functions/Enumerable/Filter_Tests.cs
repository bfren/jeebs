// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Xunit;
using static F.OptionF.Enumerable;

namespace F.OptionF_Tests.Enumerable;

public class Filter_Tests : Jeebs_Tests.Enumerable.Filter_Tests
{
	[Fact]
	public override void Test00_Maps_And_Returns_Only_Some_From_List()
	{
		Test00(list => Filter(list, null));
	}

	[Fact]
	public override void Test01_Maps_And_Returns_Matching_Some_From_List()
	{
		Test01((list, predicate) => Filter(list, predicate));
	}
}

// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;
using static F.OptionF.Enumerable;

namespace F.OptionF_Tests.Enumerable
{
	public class FilterMap_Tests : Jeebs_Tests.Enumerable.FilterMap_Tests
	{
		[Fact]
		public override void Test00_Maps_And_Returns_Only_Some_From_List()
		{
			Test00((list, map) => FilterMap(list, map, null));
		}

		[Fact]
		public override void Test01_Maps_And_Returns_Only_Some_From_List()
		{
			Test01((list, map) => FilterMap(list, map, null));
		}

		[Fact]
		public override void Test02_Returns_Matching_Some_From_List()
		{
			Test02((list, map, predicate) => FilterMap(list, map, predicate));
		}

		[Fact]
		public override void Test03_Returns_Matching_Some_From_List()
		{
			Test03((list, map, predicate) => FilterMap(list, map, predicate));
		}
	}
}

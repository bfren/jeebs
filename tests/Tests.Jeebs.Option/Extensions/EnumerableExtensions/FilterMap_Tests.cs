// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Linq;
using Xunit;

namespace Jeebs.EnumerableExtensions_Tests
{
	public class FilterMap_Tests : Jeebs_Tests.Enumerable.FilterMap_Tests
	{
		[Fact]
		public override void Test00_Maps_And_Returns_Only_Some_From_List()
		{
			Test00((list, map) => list.FilterMap(map));
		}

		[Fact]
		public override void Test01_Maps_And_Returns_Only_Some_From_List()
		{
			Test01((list, map) => list.FilterMap(map));
		}

		[Fact]
		public override void Test02_Returns_Matching_Some_From_List()
		{
			Test02((list, map, predicate) => list.FilterMap(map, predicate));
		}

		[Fact]
		public override void Test03_Returns_Matching_Some_From_List()
		{
			Test03((list, map, predicate) => list.FilterMap(map, predicate));
		}
	}
}

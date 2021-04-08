// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Linq;
using Xunit;

namespace Jeebs.EnumerableExtensions_Tests
{
	public class Filter_Tests : Jeebs_Tests.Enumerable.Filter_Tests
	{
		[Fact]
		public override void Test00_Maps_And_Returns_Only_Some_From_List()
		{
			Test00(list => list.Filter());
		}

		[Fact]
		public override void Test01_Maps_And_Returns_Matching_Some_From_List()
		{
			Test01((list, predicate) => list.Filter(predicate));
		}
	}
}

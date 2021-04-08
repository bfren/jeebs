// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.Linq;
using Xunit;

namespace Jeebs.EnumerableExtensions_Tests
{
	public class FirstOrNone_Tests : Jeebs_Tests.Enumerable.FirstOrNone_Tests
	{
		[Fact]
		public override void Test00_Empty_List_Returns_None_With_ListIsEmptyMsg()
		{
			Test00(list => list.FirstOrNone());
		}

		[Fact]
		public override void Test01_No_Matching_Items_Returns_None_With_FirstItemIsNullMsg()
		{
			Test01((list, predicate) => list.FirstOrNone(predicate));
		}

		[Fact]
		public override void Test02_Returns_First_Element()
		{
			Test02(list => list.FirstOrNone());
		}

		[Fact]
		public override void Test03_Returns_First_Matching_Element()
		{
			Test03((list, predicate) => list.FirstOrNone(predicate));
		}
	}
}

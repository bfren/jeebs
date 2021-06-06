// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;
using static F.OptionF.Enumerable;

namespace F.OptionF_Tests.Enumerable
{
	public class SingleOrNone_Tests : Jeebs_Tests.Enumerable.SingleOrNone_Tests
	{
		[Fact]
		public override void Test00_Empty_List_Returns_None_With_ListIsEmptyMsg()
		{
			Test00(list => SingleOrNone(list, null));
		}

		[Fact]
		public override void Test01_Multiple_Items_Returns_None_With_MultipleItemsMsg()
		{
			Test01(list => SingleOrNone(list, null));
		}

		[Fact]
		public override void Test02_No_Matching_Items_Returns_None_With_NoMatchingItemsMsg()
		{
			Test02((list, predicate) => SingleOrNone(list, predicate));
		}

		[Fact]
		public override void Test03_Null_Item_Returns_None_With_NullItemMsg()
		{
			Test03((list, predicate) => SingleOrNone(list, predicate));
		}

		[Fact]
		public override void Test04_Returns_Single_Element()
		{
			Test04(list => SingleOrNone(list, null));
		}

		[Fact]
		public override void Test05_Returns_Single_Matching_Element()
		{
			Test05((list, predicate) => SingleOrNone(list, predicate));
		}
	}
}

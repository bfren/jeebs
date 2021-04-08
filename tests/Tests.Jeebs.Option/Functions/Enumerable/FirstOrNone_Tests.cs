// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Xunit;
using static F.OptionF.Enumerable;

namespace F.OptionF_Tests.Enumerable
{
	public class FirstOrNone_Tests : Jeebs_Tests.Enumerable.FirstOrNone_Tests
	{
		[Fact]
		public override void Test00_Empty_List_Returns_None_With_ListIsEmptyMsg()
		{
			Test00(list => FirstOrNone(list, null));
		}

		[Fact]
		public override void Test01_No_Matching_Items_Returns_None_With_FirstItemIsNullMsg()
		{
			Test01((list, predicate) => FirstOrNone(list, predicate));
		}

		[Fact]
		public override void Test02_Returns_First_Element()
		{
			Test02(list => FirstOrNone(list, null));
		}

		[Fact]
		public override void Test03_Returns_First_Matching_Element()
		{
			Test03((list, predicate) => FirstOrNone(list, predicate));
		}
	}
}

// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Linq;
using Xunit;

namespace Jeebs.EnumerableExtensions_Tests
{
	public class ElementAtOrNone_Tests : Jeebs_Tests.Enumerable.ElementAtOrNone_Tests
	{
		[Fact]
		public override void Test00_Empty_List_Returns_None_With_ListIsEmptyMsg()
		{
			Test00((list, index) => list.ElementAtOrNone(index));
		}

		[Fact]
		public override void Test01_No_Value_At_Index_Returns_None_With_ElementAtIsNullMsg()
		{
			Test01((list, index) => list.ElementAtOrNone(index));
		}

		[Fact]
		public override void Test02_Value_At_Index_Returns_Some_With_Value()
		{
			Test02((list, index) => list.ElementAtOrNone(index));
		}
	}
}

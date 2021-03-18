// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs.Linq;
using Xunit;
using static F.OptionF.Enumerable.Msg;

namespace Jeebs.EnumerableExtensions_Tests
{
	public class ElementAtOrNone_Tests
	{
		[Fact]
		public void Empty_List_Returns_None_With_ListIsEmptyMsg()
		{
			// Arrange
			var list = Array.Empty<int>();

			// Act
			var result = list.ElementAtOrNone(0);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<ListIsEmptyMsg>(none);
		}

		[Fact]
		public void No_Value_At_Index_Returns_None_With_ElementAtIsNullMsg()
		{
			// Arrange
			var list = new int?[] { F.Rnd.Int, F.Rnd.Int, F.Rnd.Int };

			// Act
			var result = list.ElementAtOrNone(4);

			// Assert
			var none = result.AssertNone();
			Assert.IsType<ElementAtIsNullMsg>(none);
		}

		[Fact]
		public void Returns_Element_At_Index()
		{
			// Arrange
			var value = F.Rnd.Int;
			var list = new[] { F.Rnd.Int, value, F.Rnd.Int };

			// Act
			var result = list.ElementAtOrNone(1);

			// Assert
			var some = result.AssertSome();
			Assert.Equal(value, some);
		}
	}
}

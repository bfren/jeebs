// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using System;
using Jeebs;
using Xunit;
using static F.OptionF.Enumerable;
using static F.OptionF.Enumerable.Msg;

namespace F.OptionFEnumerable_Tests
{
	public class ElementAtOrNone_Tests
	{
		[Fact]
		public void Empty_List_Returns_None_With_ListIsEmptyMsg()
		{
			// Arrange
			var list = Array.Empty<int>();

			// Act
			var result = ElementAtOrNone(list, 0);

			// Assert
			var none = Assert.IsType<None<int>>(result);
			Assert.IsType<ListIsEmptyMsg>(none.Reason);
		}

		[Fact]
		public void No_Value_At_Index_Returns_None_With_ElementAtIsNullMsg()
		{
			// Arrange
			var list = new int?[] { Rnd.Int, Rnd.Int, Rnd.Int };

			// Act
			var result = ElementAtOrNone(list, 4);

			// Assert
			var none = Assert.IsType<None<int?>>(result);
			Assert.IsType<ElementAtIsNullMsg>(none.Reason);
		}

		[Fact]
		public void Returns_Element_At_Index()
		{
			// Arrange
			var value = Rnd.Int;
			var list = new[] { Rnd.Int, value, Rnd.Int };

			// Act
			var result = ElementAtOrNone(list, 1);

			// Assert
			var some = Assert.IsType<Some<int>>(result);
			Assert.Equal(value, some.Value);
		}
	}
}

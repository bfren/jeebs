using System;
using System.Collections.Generic;
using System.Text;
using Jeebs.Data.Enums;
using NSubstitute;
using Xunit;
using static Jeebs.Data.Adapter_Tests.Adapter;

namespace Jeebs.Data.Adapter_Tests
{
	public class GetSortOrder_Tests
	{
		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void No_Column_Returns_Empty(string input)
		{
			// Arrange
			var adapter = GetAdapter();

			// Act
			var result = adapter.GetSortOrder(input, SortOrder.Ascending);

			// Assert
			Assert.Equal(string.Empty, result);
		}

		[Theory]
		[InlineData("one", SortOrder.Ascending, "one ASC")]
		[InlineData("two", SortOrder.Descending, "two DESC")]
		public void Returns_Column_And_Order(string column, SortOrder order, string expected)
		{
			// Arrange
			var adapter = GetAdapter();

			// Act
			var result = adapter.GetSortOrder(column, order);

			// Assert
			Assert.Equal(expected, result);
		}
	}
}

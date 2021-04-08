// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jeebs.WordPress.Data.Enums;
using Xunit;
using static Jeebs.WordPress.Data.Adapter_Tests.Adapter;

namespace Jeebs.WordPress.Data.Adapter_Tests
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

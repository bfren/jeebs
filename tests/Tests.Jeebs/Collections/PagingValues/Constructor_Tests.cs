// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Collections.PagingValues_Tests;

public class Constructor_Tests
{
	[Fact]
	public void No_Page_Or_Items_Current_Page_Equals_1()
	{
		// Arrange
		var pageNoItems = new PagingValues(items: 0, page: 1);
		var itemsNoPage = new PagingValues(items: 1, page: 0);

		// Act

		// Assert
		Assert.Equal(1U, pageNoItems.Page);
		Assert.Equal(1U, itemsNoPage.Page);
	}

	[Theory]
	[InlineData(1, 1, 10)]
	[InlineData(2, 11, 20)]
	[InlineData(3, 21, 25)]
	public void Correct_First_And_Last_Items(ulong page, ulong firstItem, ulong lastItem)
	{
		// Arrange
		var values = new PagingValues(items: 25, page: page);

		// Act

		// Assert
		Assert.Equal(firstItem, values.FirstItem);
		Assert.Equal(lastItem, values.LastItem);
	}

	[Theory]
	[InlineData(2, 25, 1, 3)]
	[InlineData(4, 478, 1, 10)]
	[InlineData(17, 478, 11, 20)]
	[InlineData(45, 478, 41, 48)]
	public void Correct_Upper_And_Lower_Pages(ulong page, ulong items, ulong lowerPage, ulong upperPage)
	{
		// Arrange
		var values = new PagingValues(items: items, page: page);

		// Act

		// Assert
		Assert.Equal(lowerPage, values.LowerPage);
		Assert.Equal(upperPage, values.UpperPage);
	}
}

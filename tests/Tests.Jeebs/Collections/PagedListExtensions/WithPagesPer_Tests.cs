﻿// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Collections.PagedListExtensions_Tests;

public class WithPagesPer_Tests
{
	[Fact]
	public void Creates_New_PagedList_With_Updated_PagesPer()
	{
		// Arrange
		var items = 400;
		var paging = new PagingValues(
			items: (ulong)items,
			page: Rnd.NumberF.GetUInt64(1, 20),
			itemsPer: Rnd.NumberF.GetUInt64(5, 10),
			pagesPer: Rnd.NumberF.GetUInt64(5, 15)
		);
		var list = new PagedList<int>(paging, Enumerable.Range(0, items));
		var newPagesPer = paging.PagesPer + 1;

		// Act
		var result = list.WithPagesPer(newPagesPer);

		// Assert
		Assert.NotSame(list, result);
		Assert.NotEqual(paging.PagesPer, result.Values.PagesPer);
	}
}

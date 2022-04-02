// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Collections.PagedList_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Creates_Default_Values()
	{
		// Arrange
		var defaultValues = new PagingValues();

		// Act
		var result = new PagedList<int>();

		// Assert
		Assert.Equal(defaultValues, result.Values);
	}

	[Fact]
	public void Sets_Values()
	{
		// Arrange
		var values = Substitute.For<IPagingValues>();

		// Act
		var result = new PagedList<int>(values, Array.Empty<int>());

		// Assert
		Assert.Same(values, result.Values);
	}
}

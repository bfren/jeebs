// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.Enums.SearchPostFields_Tests;

public class All_Tests
{
	[Fact]
	public void Zero_Returns_None()
	{
		// Arrange

		// Act
		var result = (SearchPostField)0;

		// Assert
		Assert.Equal(SearchPostField.None, result);
	}

	[Theory]
	[InlineData(SearchPostField.Title)]
	[InlineData(SearchPostField.Slug)]
	[InlineData(SearchPostField.Content)]
	[InlineData(SearchPostField.Excerpt)]
	public void Matches_All_Fields(SearchPostField field)
	{
		// Arrange

		// Act
		var result = SearchPostField.All & field;

		// Assert
		Assert.Equal(field, result);
	}
}

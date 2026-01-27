// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.ContentFilters.ReplaceWpContent_Tests;

public class Execute_Tests
{
	[Fact]
	public void Replaces_Content()
	{
		// Arrange
		var from = Rnd.Str;
		var to = Rnd.Str;

		// Act
		var result = ReplaceWpContent.Create(from, to).Execute(from);

		// Assert
		Assert.Equal(to, result);
	}
}

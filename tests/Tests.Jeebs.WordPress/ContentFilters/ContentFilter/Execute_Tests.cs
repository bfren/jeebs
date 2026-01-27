// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.WordPress.ContentFilters.ContentFilter_Tests;

public class Execute_Tests
{
	[Fact]
	public void Runs_Filter()
	{
		// Arrange
		var filter = Substitute.For<Func<string, string>>();
		var content = Rnd.Str;

		var contentFilter = Substitute.ForPartsOf<ContentFilter>(filter);

		// Act
		contentFilter.Execute(content);

		// Assert
		filter.Received().Invoke(content);
	}
}

// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.WordPress.Enums;
using Xunit;

namespace Jeebs.WordPress.TermList_Tests;

public class Constructor_Tests
{
	[Fact]
	public void Sets_Properties()
	{
		// Arrange
		var taxonomy = Taxonomy.Blank;

		// Act
		var result = new TermList(taxonomy);

		// Assert
		Assert.Same(taxonomy, result.Taxonomy);
	}
}

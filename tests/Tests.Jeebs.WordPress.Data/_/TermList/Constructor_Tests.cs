// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.WordPress.Data.Enums;
using Xunit;

namespace Jeebs.WordPress.Data.TermList_Tests
{
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
}

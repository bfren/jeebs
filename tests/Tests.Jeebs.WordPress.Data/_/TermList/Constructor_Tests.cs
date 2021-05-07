// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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

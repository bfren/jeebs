// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;

namespace Jeebs.WordPress.Data.Querying.QueryParts_Tests
{
	public class Constructor_Tests
	{
		[Fact]
		public void Sets_From()
		{
			// Arrange
			var from = F.Rnd.Str;

			// Act
			var result = new QueryParts(from);

			// Assert
			Assert.Equal(from, result.From);
		}
	}
}

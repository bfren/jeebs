// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

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

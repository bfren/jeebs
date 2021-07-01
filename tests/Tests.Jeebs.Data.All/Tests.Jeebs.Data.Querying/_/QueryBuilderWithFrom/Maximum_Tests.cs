// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Jeebs.Data.Mapping;
using NSubstitute;
using Xunit;

namespace Jeebs.Data.Querying.QueryBuilderWithFrom_Tests
{
	public class Maximum_Tests
	{
		[Fact]
		public void Sets_Maximum()
		{
			// Arrange
			var table = Substitute.For<ITable>();
			var builder = new QueryBuilderWithFrom(table);
			var value = F.Rnd.Ulng;

			// Act
			var result = builder.Maximum(value);

			// Assert
			var from = Assert.IsType<QueryBuilderWithFrom>(result);
			Assert.Equal(value, from.Parts.Maximum);
		}
	}
}

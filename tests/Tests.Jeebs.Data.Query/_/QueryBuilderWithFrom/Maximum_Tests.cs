// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.Query.QueryBuilderWithFrom_Tests;

public class Maximum_Tests
{
	[Fact]
	public void Sets_Maximum()
	{
		// Arrange
		var table = Substitute.For<ITable>();
		var builder = new QueryBuilderWithFrom(table);
		var value = Rnd.ULng;

		// Act
		var result = builder.Maximum(value);

		// Assert
		var from = Assert.IsType<QueryBuilderWithFrom>(result);
		Assert.Equal(value, from.Parts.Maximum);
	}
}

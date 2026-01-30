// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.QueryBuilder.QueryBuilder_Tests;

public class From_Tests
{
	[Fact]
	public void Creates_QueryBuilderWithFrom_Using_Table()
	{
		// Arrange
		var builder = new QueryBuilder();
		var table = new TestTable();

		// Act
		var result = builder.From(table);

		// Assert
		var from = Assert.IsType<QueryBuilderWithFrom>(result);
		var single = Assert.Single(from.Tables);
		Assert.Same(table, single);
	}

	[Fact]
	public void Creates_QueryBuilderWithFrom_With_New_Table()
	{
		// Arrange
		var builder = new QueryBuilder();

		// Act
		var result = builder.From<TestTable>();

		// Assert
		var from = Assert.IsType<QueryBuilderWithFrom>(result);
		var single = Assert.Single(from.Tables);
		Assert.IsType<TestTable>(single);
	}

	public sealed record class TestTable() : Table(Rnd.Str);
}

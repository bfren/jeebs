// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.Query.QueryBuilderWithFrom_Tests;

public class AddTable_Tests
{
	[Fact]
	public void Table_Not_Added_Adds_Table()
	{
		// Arrange
		var table = Substitute.For<ITable>();
		var builder = new QueryBuilderWithFrom(table);

		// Act
		builder.AddTable<TestTable>();

		// Assert
		Assert.Collection(builder.Tables,
			x => Assert.Same(table, x),
			x => Assert.IsType<TestTable>(x)
		);
	}

	[Fact]
	public void Table_Already_Added_Does_Nothing()
	{
		// Arrange
		var table = new TestTable();
		var builder = new QueryBuilderWithFrom(table);

		// Act
		builder.AddTable<TestTable>();

		// Assert
		var single = Assert.Single(builder.Tables);
		Assert.IsType<TestTable>(single);
	}

	public sealed record class TestTable() : Table(Rnd.Str);
}

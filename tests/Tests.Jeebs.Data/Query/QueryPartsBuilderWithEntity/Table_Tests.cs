// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

namespace Jeebs.Data.Query.QueryPartsBuilderWithEntity_Tests;

public class Table_Tests : QueryPartsBuilderWithEntity_Tests
{
	[Fact]
	public void Returns_Map_Table()
	{
		// Arrange
		var (builder, v) = Setup();
		var table = Substitute.For<ITable>();
		v.Map.Table.Returns(table);

		// Act
		var result = builder.Table;

		// Assert
		Assert.Same(table, result);
	}
}

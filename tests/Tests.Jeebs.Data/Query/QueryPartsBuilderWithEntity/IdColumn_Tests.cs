// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map;

namespace Jeebs.Data.Query.QueryPartsBuilderWithEntity_Tests;

public class IdColumn_Tests : QueryPartsBuilderWithEntity_Tests
{
	[Fact]
	public void Returns_Map_IdColumn()
	{
		// Arrange
		var (builder, v) = Setup();
		var idColumn = Substitute.For<IColumn>();
		v.Map.IdColumn.Returns(idColumn);

		// Act
		var result = builder.IdColumn;

		// Assert
		Assert.Same(idColumn, result);
	}
}

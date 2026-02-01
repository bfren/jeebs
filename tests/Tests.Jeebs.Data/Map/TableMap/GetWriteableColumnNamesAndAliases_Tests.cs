// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Functions;

namespace Jeebs.Data.Map.TableMap_Tests;

public class GetWriteableColumnNamesAndAliases_Tests
{
	[Fact]
	public void No_Writeable_Columns_Returns_None_With_NoWriteableColumnsFoundMsg()
	{
		// Arrange
		var table = new FooUnwriteableTable();
		var columns = DataF.GetColumns<FooUnwriteableTable, FooUnwriteable>(table).Unsafe().Unwrap();
		var map = new TableMap(table, columns, GetColumnNames_Tests.Get().column);

		// Act
		var result = map.GetWriteableColumnNamesAndAliases();

		// Assert
		_ = result.AssertFailure("No writeable columns found.");
	}

	[Fact]
	public void Returns_Writeable_Names_And_Aliases()
	{
		// Arrange
		var table = new FooTable();
		var columns = DataF.GetColumns<FooTable, Foo>(table).Unsafe().Unwrap();
		var map = new TableMap(table, columns, GetColumnNames_Tests.Get().column);

		// Act
		var result = map.GetWriteableColumnNamesAndAliases();

		// Assert
		var (names, aliases) = result.AssertOk();
		Assert.Collection(names,
			x => Assert.Equal(table.Bar0, x),
			x => Assert.Equal(table.Bar1, x)
		);
		Assert.Collection(aliases,
			x => Assert.Equal(nameof(table.Bar0), x),
			x => Assert.Equal(nameof(table.Bar1), x)
		);
	}
}

// Jeebs Unit Tests
// Copyright (c) bfren - licensed under https://mit.bfren.dev/2013

using Jeebs.Data.Map.Functions;
using static Jeebs.Data.Map.TableMap.M;

namespace Jeebs.Data.Map.TableMap_Tests;

public class GetWriteableColumnNamesAndAliases_Tests
{
	[Fact]
	public void No_Writeable_Columns_Returns_None_With_NoWriteableColumnsFoundMsg()
	{
		// Arrange
		var table = new FooUnwriteableTable();
		var columns = MapF.GetColumns<FooUnwriteableTable, FooUnwriteable>(table).UnsafeUnwrap();
		var map = new TableMap(table, columns, GetColumnNames_Tests.Get().column);

		// Act
		var result = map.GetWriteableColumnNamesAndAliases();

		// Assert
		result.AssertNone().AssertType<NoWriteableColumnsFoundMsg>();
	}

	[Fact]
	public void Returns_Writeable_Names_And_Aliases()
	{
		// Arrange
		var table = new FooTable();
		var columns = MapF.GetColumns<FooTable, Foo>(table).UnsafeUnwrap();
		var map = new TableMap(table, columns, GetColumnNames_Tests.Get().column);

		// Act
		var result = map.GetWriteableColumnNamesAndAliases();

		// Assert
		var (names, aliases) = result.AssertSome();
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

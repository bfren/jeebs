// Jeebs Unit Tests
// Copyright (c) bfren.uk - licensed under https://mit.bfren.uk/2013

using Xunit;
using static F.DataF.MappingF;
using static Jeebs.Data.Mapping.TableMap.Msg;

namespace Jeebs.Data.Mapping.TableMap_Tests
{
	public class GetWriteableColumnNamesAndAliases_Tests
	{
		[Fact]
		public void No_Writeable_Columns_Returns_None_With_NoWriteableColumnsFoundMsg()
		{
			// Arrange
			var table = new FooUnwriteableTable();
			var columns = GetMappedColumns<FooUnwriteable>(table).UnsafeUnwrap();
			var map = new TableMap(table, columns, GetColumnNames_Tests.Get().column);

			// Act
			var result = map.GetWriteableColumnNamesAndAliases();

			// Assert
			var none = result.AssertNone();
			Assert.IsType<NoWriteableColumnsFoundMsg>(none);
		}

		[Fact]
		public void Returns_Writeable_Names_And_Aliases()
		{
			// Arrange
			var table = new FooTable();
			var columns = GetMappedColumns<Foo>(table).UnsafeUnwrap();
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
}

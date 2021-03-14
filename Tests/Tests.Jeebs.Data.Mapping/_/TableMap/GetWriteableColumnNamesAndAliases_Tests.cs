// Jeebs Unit Tests
// Copyright (c) bcg|design - licensed under https://mit.bcgdesign.com/2013

using Jx.Data.Mapping;
using Xunit;

namespace Jeebs.Data.Mapping.TableMap_Tests
{
	public class GetWriteableColumnNamesAndAliases_Tests
	{
		[Fact]
		public void No_Writeable_Columns_Throws_MappingException()
		{
			// Arrange
			using var svc = new MapService();
			var table = new FooUnwriteableTable();
			var columns = svc.GetMappedColumns<FooUnwriteable, FooUnwriteableTable>();
			var map = new TableMap(table.ToString(), columns, GetColumnNames_Tests.Get().column);

			// Act
			void action() => map.GetWriteableColumnNamesAndAliases();

			// Assert
			var ex = Assert.Throws<NoWriteableColumnsException>(action);
			Assert.Equal(string.Format(NoWriteableColumnsException.Format, table), ex.Message);
		}

		[Fact]
		public void Returns_Writeable_Names_And_Aliases()
		{
			// Arrange
			using var svc = new MapService();
			var table = new FooTable();
			var columns = svc.GetMappedColumns<Foo, FooTable>();
			var map = new TableMap(table.ToString(), columns, GetColumnNames_Tests.Get().column);

			// Act
			var (names, aliases) = map.GetWriteableColumnNamesAndAliases();

			// Assert
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
